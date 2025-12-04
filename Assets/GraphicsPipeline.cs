using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class GraphicsPipeline : MonoBehaviour
{
    StreamWriter writer;
    public GameObject screenGO;
    Renderer screen;
    Model myModel;

    public Material matP;
    Texture2D texture_file;

    float RotationSpeed = 50f;
    void Start()
    {
        screen = screenGO.GetComponent<Renderer>();
        writer = new StreamWriter("Data.txt", false);

        myModel = new();
        texture_file = (Texture2D)matP.mainTexture;

        //myModel.CreateUnityGameObject();
        List<Vector3> verts3 = myModel.vertices;

        List<Vector4> verts = ConvertToHOM(verts3);
        WriteVectorsToFile(verts, "Vertices of my letter", "-------------------");

        Matrix4x4 rotationMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(36, new Vector3(20, -5, -5).normalized), Vector3.one);
        WriteMatrixToFile(rotationMatrix, "Rotation Matrix", "------------------");

        List<Vector4> imageAfterRotation = ApplyTransformation(verts, rotationMatrix);
        WriteVectorsToFile(imageAfterRotation, "Image after Rotation", "------------------");

        Matrix4x4 scaleMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(20, 5, 5));
        WriteMatrixToFile(scaleMatrix, "Scale Matrix", "------------------");

        var imageAfterScale = ApplyTransformation(imageAfterRotation, scaleMatrix);
        WriteVectorsToFile(imageAfterScale, "Image after Scale", "------------------");

        Matrix4x4 translationMatrix = Matrix4x4.TRS(new Vector3(1f, 2f, -3f), Quaternion.identity, Vector3.one);
        WriteMatrixToFile(translationMatrix, "Translation Matrix", "------------------");

        var imageAfterTranslation = ApplyTransformation(imageAfterScale, translationMatrix);
        WriteVectorsToFile(imageAfterTranslation, "Image after Translation", "------------------");

        Matrix4x4 matrixOfTransformations = translationMatrix * scaleMatrix * rotationMatrix;
        WriteMatrixToFile(matrixOfTransformations, "Transformation Matrix", "------------------");

        var imageAfterTransformations = ApplyTransformation(verts, matrixOfTransformations);
        WriteVectorsToFile(imageAfterTransformations, "Image After Transformations", "------------------");

        Matrix4x4 viewingMatrix = Matrix4x4.LookAt(new Vector3(22f, -5f, 45f), new Vector3(-5f, 20f, -5f), new Vector3(-4f, -5f, 20f).normalized);
        WriteMatrixToFile(viewingMatrix, "Viewing Matrix", "------------------");

        var imageAfterViewing = ApplyTransformation(imageAfterTranslation, viewingMatrix);
        WriteVectorsToFile(imageAfterViewing, "Image After Viewing Matrix", "------------------");

        Matrix4x4 projectionMatrix = Matrix4x4.Perspective(90, 1, 1, 1000);
        WriteMatrixToFile(projectionMatrix, "Projection Matrix", "------------------");

        var imageAfterProjection = ApplyTransformation(imageAfterViewing, projectionMatrix);
        WriteVectorsToFile(imageAfterProjection, "Final Image", "------------------");

        Matrix4x4 matrixOfEverything = projectionMatrix * viewingMatrix * matrixOfTransformations;
        WriteMatrixToFile(matrixOfEverything, "Matrix of Everything", "------------------");

        var imageAfterEverything = ApplyTransformation(verts, matrixOfEverything);
        WriteVectorsToFile(imageAfterEverything, "Image After Everything", "------------------");

        writer.Close();

        Vector2 s = new(2, 4);
        Vector2 e = new(3, -3);
        print(Intercept(s, e, 0));

        if (LineClip(ref s, ref e))
        {
            print($"Clipped Line Start: {s}, End: {e}");
        }
        else
        {
            print("Line is outside the viewport");
        }
    }

    private void DrawLine(Vector2Int start, Vector2Int end, Texture2D texture)
    {
        Vector2 sNDC = PixelToNDC(start, texture.width, texture.height);
        Vector2 eNDC = PixelToNDC(end, texture.width, texture.height);

        if (LineClip(ref sNDC, ref eNDC))
        {
            Vector2Int clippedStart = NDCToPixel(sNDC, texture.width, texture.height);
            Vector2Int clippedEnd = NDCToPixel(eNDC, texture.width, texture.height);

            SetPixels(Bresenham(clippedStart, clippedEnd), texture);
        }
    }

    Vector2 PixelToNDC(Vector2Int p, int width, int height)
    {
        float x = (2f * p.x) / (width - 1) - 1f;
        float y = (2f * p.y) / (height - 1) - 1f;
        return new Vector2(x, y);
    }

    Vector2Int NDCToPixel(Vector2 v, int width, int height)
    {
        int x = Mathf.RoundToInt((v.x + 1f) * 0.5f * (width - 1));
        int y = Mathf.RoundToInt((v.y + 1f) * 0.5f * (height - 1));
        return new Vector2Int(x, y);
    }

    private void SetPixels(List<Vector2Int> vector2Ints, Texture2D fb)
    {
        foreach (Vector2Int p in vector2Ints)
        {
            fb.SetPixel(p.x, p.y, Color.red);
        }
    }

    private List<Vector2Int> Pixelize(List<Vector4> projectedVerts, int resX, int resY)
    {
        List<Vector2Int> output = new();
        foreach (Vector4 v in projectedVerts)
        {
            Vector2 ndc = new(v.x / v.w, v.y / v.w);
            Vector2Int pixel = new((int)((ndc.x + 1) * 0.5f * (resX - 1)), (int)((ndc.y + 1) * 0.5f * (resY - 1)));
            output.Add(pixel);
        }

        return output;
    }

    private List<Vector4> ConvertToHOM(List<Vector3> verts3)
    {
        List<Vector4> output = new();

        foreach (Vector3 v in verts3)
        {
            output.Add(new Vector4(v.x, v.y, v.z, 1.0f));
        }
        return output;
    }

    private List<Vector4> ApplyTransformation(List<Vector4> verts, Matrix4x4 tranformMatrix)
    {
        List<Vector4> output = new();
        foreach (Vector4 v in verts)
        { output.Add(tranformMatrix * v); }

        return output;
    }

    private void WriteMatrixToFile(Matrix4x4 matrix, string before, string after)
    {
        writer.WriteLine(before);
        for (int i = 0; i < 4; i++)
        {
            Vector4 v = matrix.GetRow(i);
            writer.WriteLine($"({v.x}, {v.y}, {v.z}, {v.w})");
        }
        writer.WriteLine(after);
    }

    private void WriteVectorsToFile(List<Vector4> verts, string before, string after)
    {
        writer.WriteLine(before);
        foreach (Vector4 v in verts)
        {
            writer.WriteLine($"({v.x}, {v.y}, {v.z}, {v.w})");
        }
        writer.WriteLine(after);
    }

    bool LineClip(ref Vector2 start, ref Vector2 end)
    {
        OutCode startOutCode = new(start);
        OutCode endOutCode = new(end);
        OutCode inViewport = new();

        if ((startOutCode + endOutCode) == inViewport) return true;

        if ((startOutCode * endOutCode) != inViewport) return false;

        if (startOutCode == inViewport)
        {
            return LineClip(ref end, ref start);
        }

        if (startOutCode.up)
        {
            start = Intercept(start, end, 0);
            return LineClip(ref start, ref end);
        }
        else if (startOutCode.down)
        {
            start = Intercept(start, end, 1);
            return LineClip(ref start, ref end);
        }
        else if (startOutCode.left)
        {
            start = Intercept(start, end, 2);
            return LineClip(ref start, ref end);
        }
        else if (startOutCode.right)
        {
            start = Intercept(start, end, 3);
            return LineClip(ref start, ref end);
        }

        return false;
    }

    Vector2 Intercept(Vector2 s, Vector2 e, int edgeIndex)
    {
        if (e.x != s.x)
        {
            float m = (e.y - s.y) / (e.x - s.x);

            switch (edgeIndex)
            {
                case 0:
                    if (m != 0)
                    {
                        return new Vector2(s.x + (1 / m) * (1 - s.y), 1);
                    }
                    else
                    {
                        if (s.y == 1)
                            return s;
                        else
                            return new Vector2(float.NaN, float.NaN);
                    }
                case 1:
                    if (m != 0)
                    {
                        return new Vector2(s.x + (1 / m) * (-1 - s.y), -1);
                    }
                    else
                    {
                        if (s.y == -1)
                            return s;
                        else
                            return new Vector2(float.NaN, float.NaN);
                    }
                case 2:
                    {
                        float y = s.y + m * (-1 - s.x);
                        return new Vector2(-1, y);
                    }
                default:
                    return new Vector2(1, s.y + m * (1 - s.x));
            }
        }
        else
        {
            switch (edgeIndex)
            {
                case 0:
                    return new Vector2(s.x, 1);

                case 1:
                    return new Vector2(s.x, -1);

                case 2:
                    if (s.x == -1)
                        return s;
                    else
                        return new Vector2(float.NaN, float.NaN);

                default:
                    if (s.x == 1)
                        return s;
                    else
                        return new Vector2(float.NaN, float.NaN);
            }
        }
    }

    private List<Vector2Int> Bresenham(Vector2Int Start, Vector2Int End)
    {
        List<Vector2Int> points = new();

        int dx = End.x - Start.x;

        if (dx < 0) return Bresenham(End, Start);

        int dy = End.y - Start.y;

        if (dy < 0)
            return NegY(Bresenham(NegY(Start), NegY(End)));

        if (dy > dx)
            return SwapXY(Bresenham(SwapXY(Start), SwapXY(End)));


        int neg = 2 * (dy - dx);
        int pos = 2 * dy;
        int p = 2 * dy - dx;

        for (int x = Start.x, y = Start.y; x <= End.x; x++)
        {
            points.Add(new Vector2Int(x, y));
            if (p < 0)
            {
                p += pos;
            }
            else
            {
                y++;
                p += neg;
            }
        }

        return points;
    }

    private List<Vector2Int> SwapXY(List<Vector2Int> l)
    {
        List<Vector2Int> hold = new();
        foreach (Vector2Int v in l)
            hold.Add(SwapXY(v));

        return hold;
    }

    private Vector2Int SwapXY(Vector2Int v)
    {
        return new Vector2Int(v.y, v.x);
    }

    private List<Vector2Int> NegY(List<Vector2Int> l)
    {
        List<Vector2Int> hold = new();
        foreach (Vector2Int v in l)
            hold.Add(NegY(v));

        return hold;
    }

    private Vector2Int NegY(Vector2Int v)
    {
        return new Vector2Int(v.x,-v.y);
    }

    private void Update()
    {
        List<Vector4> verts4 = ConvertToHOM(myModel.vertices);

        Matrix4x4 worldMatrix = Matrix4x4.TRS(new Vector3(0, 0, 10), Quaternion.identity, Vector3.one);
        Matrix4x4 rot = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(Time.time * RotationSpeed, new Vector3(0, 1, 0)), Vector3.one);
        worldMatrix *= rot;
        Matrix4x4 viewMatrix = Matrix4x4.LookAt(new Vector3(0, 0, 0), new Vector3(0, 0, 10), new Vector3(0, 1, 0));
        Matrix4x4 projectionMat = Matrix4x4.Perspective(90, 1, 1, 1000);
        Matrix4x4 mvp = projectionMat * viewMatrix * worldMatrix;
        List<Vector4> projectedVerts = ApplyTransformation(verts4, mvp);
        List<Vector2Int> pixelPoints = Pixelize(projectedVerts, 512, 512);

        List<Vector3Int> faces = myModel.faces;
        Texture2D fb = new Texture2D(512, 512);

        for (int i = 0; i < faces.Count; i++)
        {
            Vector3Int face = faces[i];
            Vector3Int texIdx = myModel.texture_index_list[i];

            Vector2 a_t = myModel.texture_coordinates[texIdx.x];
            Vector2 b_t = myModel.texture_coordinates[texIdx.y];
            Vector2 c_t = myModel.texture_coordinates[texIdx.z];

            Vector2 a2 = pixelPoints[face.x];
            Vector2 b2 = pixelPoints[face.y];
            Vector2 c2 = pixelPoints[face.z];

            FillTriangleTextured(a2, b2, c2, a_t, b_t, c_t, texture_file, fb);
        }

        screen.material.mainTexture = fb;
        fb.Apply();
    }

    void FillTriangleTextured
        (Vector2 a2, Vector2 b2, Vector2 c2, Vector2 a_t, 
        Vector2 b_t, Vector2 c_t, Texture2D tex, Texture2D fb)
    {
        Vector2 A = b2 - a2;
        Vector2 B = c2 - a2;

        Vector2 A_t = b_t - a_t;
        Vector2 B_t = c_t - a_t;

        float denom = A.x * B.y - A.y * B.x;
        if (Mathf.Abs(denom) < 1e-6f) return;

        int minX = (int)Mathf.Min(a2.x, Mathf.Min(b2.x, c2.x));
        int maxX = (int)Mathf.Max(a2.x, Mathf.Max(b2.x, c2.x));
        int minY = (int)Mathf.Min(a2.y, Mathf.Min(b2.y, c2.y));
        int maxY = (int)Mathf.Max(a2.y, Mathf.Max(b2.y, c2.y));

        if (minX < 0) minX = 0;
        if (minY < 0) minY = 0;
        if (maxX > fb.width - 1) maxX = fb.width - 1;
        if (maxY > fb.height - 1) maxY = fb.height - 1;

        for (int y_p = minY; y_p <= maxY; y_p++)
        {
            for (int x_p = minX; x_p <= maxX; x_p++)
            {
                float x = x_p - a2.x;
                float y = y_p - a2.y;

                float r = (x * B.y - y * B.x) / denom;
                float s = (A.x * y - x * A.y) / denom;

                if (r < 0f || s < 0f || r + s > 1f) continue;

                Vector2 texture_point = a_t + r * A_t + s * B_t;
                texture_point *= 512;

                Color color = tex.GetPixel((int)texture_point.x, (int)texture_point.y);
                fb.SetPixel(x_p, y_p, color);
            }
        }
    }

}
