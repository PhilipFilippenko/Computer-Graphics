using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class GraphicsPipeline : MonoBehaviour
{
    StreamWriter writer;

    void Start()
    {
        writer = new StreamWriter("Data.txt", false);

        Model myModel = new();
        myModel.CreateUnityGameObject();
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

    private bool LineClip(ref Vector2 start, ref Vector2 end)
    {
        //Test for Trivial Acceptance
        OutCode startOutCode = new OutCode(start);
        OutCode endOutCode = new OutCode(end);
        OutCode inViewport = new OutCode();

        if ((startOutCode + endOutCode) == inViewport) return true;

        //Test for Trivial Rejection
        if ((startOutCode * endOutCode) != inViewport) return false;

        //Test for Intersection
        
    }
}
