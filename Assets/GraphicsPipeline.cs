using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class GraphicsPipeline : MonoBehaviour
{
    StreamWriter writer;

    void Start()
    {
        writer = new StreamWriter("Data.txt", false);

        Model myModel = new();
        myModel.CreateUnityGameObject();
        List<Vector3> verts3 = myModel.vertices;

        List<Vector4> verts = convertToHOM(verts3);

        writeVectorsToFile(verts, "Vertices of my letter", "-------------------");

        Matrix4x4 rotationMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(36, new Vector3(20, -5, -5).normalized), Vector3.one);
        writeMatrixToFile(rotationMatrix, "Rotation Matrix", "------------------");

        List<Vector4> imageAfterRotation = applyTransformation(verts, rotationMatrix);

        Matrix4x4 scaleMatrix =
            Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(20, 5, 5));

        writer.Close();
;    }

    private List<Vector4> convertToHOM(List<Vector3> verts3)
    {
        List<Vector4> output = new List<Vector4>();

        foreach (Vector3 v in verts3)
        {
            output.Add(new Vector4(v.x, v.y, v.z, 1.0f));
        }
        return output;
    }

    private List<Vector4> applyTransformation(List<Vector4> verts, Matrix4x4 tranformMatrix)
    {
        List<Vector4> output = new List<Vector4>();
        foreach (Vector4 v in verts)
        { output.Add(tranformMatrix * v); }

        return output;
    }

    private void writeMatrixToFile(Matrix4x4 matrix, string before, string after)
    {
        writer.WriteLine(before);

        for (int i = 0; i < 4; i++)
        {
            Vector4 v = matrix.GetRow(i);
            writer.WriteLine($"({v.x}, {v.y}, {v.z})");
        }
        writer.WriteLine(after);
    }

    private void writeVectorsToFile(List<Vector4> verts, string before, string after)
    {
        writer.WriteLine(before);
        foreach (Vector4 v in verts)
        {
            writer.WriteLine($"({v.x}, {v.y}, {v.z}, {v.w})");
        }
        writer.WriteLine(after);
    }
}
