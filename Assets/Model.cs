using System.Collections.Generic;
using UnityEngine;

public class Model
{
    private List<Vector2> texture_coordinates;
    internal List<Vector3> vertices;
    private List<Vector3Int> faces;
    private List<Vector3Int> texture_index_list;
    private List<Vector3> normals;

    public Model()
    {
        DefineVertices();
        DefineFaces();
        DefineUVs();
    }

    private void DefineVertices()
    {
        vertices = new List<Vector3>
        {
            new(-4f, -8f, 1f),  // 0
            new(-2f, -8f, 1f),  // 1
            new(-2f, 0f, 1f),   // 2
            new(3f, 0f, 1f),    // 3
            new(4f, 1f, 1f),    // 4
            new(4f, 7f, 1f),    // 5
            new(3f, 8f, 1f),    // 6
            new(-4f, 8f, 1f),   // 7
            new(-2f, 6f, 1f),   // 8
            new(2f, 6f, 1f),    // 9
            new(2f, 2f, 1f),    // 10
            new(-2f, 2f, 1f),   // 11
            new(-4f, -8f, -1f), // 12
            new(-2f, -8f, -1f), // 13
            new(-2f, 0f, -1f),  // 14
            new(3f, 0f, -1f),   // 15
            new(4f, 1f, -1f),   // 16
            new(4f, 7f, -1f),   // 17
            new(3f, 8f, -1f),   // 18
            new(-4f, 8f, -1f),  // 19
            new(-2f, 6f, -1f),  // 20
            new(2f, 6f, -1f),   // 21
            new(2f, 2f, -1f),   // 22
            new(-2f, 2f, -1f)   // 23
        };
    }
    private void DefineFaces()
    {
        faces = new List<Vector3Int>
        {
            //--- Front ---
            new(2, 0, 1),   // 0
            new(7, 0, 2),   // 1
            new(7, 2, 8),   // 2
            new(11, 2, 3),  // 3
            new(11, 3, 10), // 4
            new(10, 3, 4),  // 5
            new(9, 10, 4),  // 6
            new(9, 4, 5),   // 7
            new(9, 5, 6),   // 8
            new(9, 6, 8),   // 9
            new(8, 6, 7),   // 10

            //--- Back ---
            new(14, 13, 12), // 12
            new(19, 14, 12), // 13
            new(19, 20, 14), // 14
            new(23, 15, 14), // 15
            new(23, 22, 15), // 16
            new(22, 16, 15), // 17
            new(21, 16, 22), // 18
            new(21, 17, 16), // 19
            new(21, 18, 17), // 20
            new(21, 20, 18), // 21
            new(20, 19, 18), // 22
            
            //--- Left ---
            new(12, 0, 7),   // 24
            new(12, 7, 19),  // 25
            
            //--- Top ---
            new(19, 7, 6),   // 26
            new(19, 6, 18),  // 27

            //--- Bottom ---
            new(12, 1, 0),   // 28
            new(12, 13, 1),  // 29

            new(14, 3, 2),   // 30
            new(14, 15, 3),  // 31

            //--- Right ---
            new(13, 2, 1),   // 32
            new(13, 14, 2),  // 33
            new(16, 5, 4),   // 34
            new(16, 17, 5),  // 35

            new(15, 4, 3),   // 36
            new(15, 16, 4),  // 37

            new(17, 6, 5),   // 38
            new(17, 18, 6),  // 39

            //--- Inside ---
            new(11, 10, 22), // 40
            new(11, 22, 23), // 41

            new(10, 9, 21),  // 42
            new(10, 21, 22), // 43

            new(8, 21, 9),   // 44
            new(8, 20, 21),  // 45

            new(8, 11, 23),  // 46
            new(8, 23, 20),  // 47
        };

        texture_index_list = new List<Vector3Int>
        {
            // front
            new(2,0,1),
            new(7,0,2),
            new(7,2,8),
            new(11,2,3),
            new(11,3,10),
            new(10,3,4),
            new(9,10,4),
            new(9,4,5),
            new(9,5,6),
            new(9,6,8),
            new(8,6,7),

            // back
            new(14, 13, 12), 
            new(19, 14, 12), 
            new(19, 20, 14), 
            new(23, 15, 14), 
            new(23, 22, 15), 
            new(22, 16, 15), 
            new(21, 16, 22), 
            new(21, 17, 16), 
            new(21, 18, 17),
            new(21, 20, 18),
            new(20, 19, 18),

            // left
            new(47,46,44),
            new(47,44,45),

            // top
            new(50,48,49),
            new(50,49,51),

            // bottom
            new(42,41,40),
            new(42,43,41),

            // under inner
            new(34,33,32),
            new(34,35,33),

            // right bottom
            new(36,38,39),
            new(36,37,38),

            // right
            new(30,29,31),
            new(30,28,29),

            // small right bottom
            new(24,26,27),
            new(24,25,26),

            // small right top
            new(24,26,27),
            new(24,25,26),

            // inside
            new(0,0,0),
            new(0,0,0),
            new(0,0,0),
            new(0,0,0),
            new(0,0,0),
            new(0,0,0),
            new(0,0,0),
            new(0,0,0),
        };

        normals = new List<Vector3>
        {
            //--- Front ---
            new(0, 0, 1),  // 0
            new(0, 0, 1),  // 1
            new(0, 0, 1),  // 2
            new(0, 0, 1),  // 3
            new(0, 0, 1),  // 4
            new(0, 0, 1),  // 5
            new(0, 0, 1),  // 6
            new(0, 0, 1),  // 7
            new(0, 0, 1),  // 8
            new(0, 0, 1),  // 9
            new(0, 0, 1),  // 10

            //--- Back ---
            new(0, 0, -1), // 12
            new(0, 0, -1), // 13
            new(0, 0, -1), // 14
            new(0, 0, -1), // 15
            new(0, 0, -1), // 16
            new(0, 0, -1), // 17
            new(0, 0, -1), // 18
            new(0, 0, -1), // 19
            new(0, 0, -1), // 20
            new(0, 0, -1), // 21
            new(0, 0, -1), // 22

            //--- Left ---
            new(-1, 0, 0), // 24
            new(-1, 0, 0), // 25

            //--- Top ---
            new(0, 1, 0),  // 26
            new(0, 1, 0),  // 27

            //--- Bottom ---
            new(0, -1, 0), // 28
            new(0, -1, 0), // 29

            new(0, -1, 0), // 30
            new(0, -1, 0), // 31

            //--- Right ---
            new(1, 0, 0),  // 32
            new(1, 0, 0),  // 33
            new(1, 0, 0),  // 34
            new(1, 0, 0),  // 35
            new(1, 0, 0),  // 36
            new(1, 0, 0),  // 37
            new(1, 0, 0),  // 38
            new(1, 0, 0),  // 39

            //--- Inside ---
            new(0, 1, 0),  // 40
            new(0, 1, 0),  // 41

            new(-1, 0, 0), // 42
            new(-1, 0, 0), // 43

            new(0, -1, 0), // 44
            new(0, -1, 0), // 45

            new(1, 0, 0),  // 46
            new(1, 0, 0),  // 47
        };
    }

    private void DefineUVs()
    {
        float texWidth = 512f;
        float texHeight = 512f;

        Vector2 UV(float px, float py) => new Vector2(px / texWidth, 1f - py / texHeight);
        
        texture_coordinates = new List<Vector2>
    {
        // front
        UV(24f, 295f), 
        UV(60f, 295f),
        UV(60f, 153f), 
        UV(155f, 155f), 
        UV(172f, 138f),
        UV(172f, 33f),
        UV(155f, 15f),
        UV(25f, 15f), 
        UV(61f, 51f),
        UV(135f, 51f),
        UV(135f, 120f), 
        UV(61f, 120f),
        
        // back
        UV(495f, 295f),
        UV(458f, 295f),
        UV(458f, 153f),
        UV(364f, 153f),
        UV(350f, 140f),
        UV(350f, 30f),
        UV(364f, 15f),
        UV(495f, 15f),
        UV(456f, 50f),
        UV(385f, 50f),
        UV(385f, 120f),
        UV(455f, 120f),

        // small right
        UV(197f, 42f),
        UV(197f, 20f),
        UV(232f, 20f),
        UV(232f, 42f),

        // right
        UV(193f, 63f),
        UV(229f, 63f),
        UV(193f, 166f),
        UV(229f, 166f),

        // under inner
        UV(83f, 193f),
        UV(174f, 193f),
        UV(83f, 229f),
        UV(174f, 229f),

        // right bottom
        UV(80f, 399f),
        UV(80f, 258f),
        UV(116f, 258f),
        UV(116f, 399f),

        // bottom
        UV(24f, 316f),
        UV(61f, 316f),
        UV(24f, 352f),
        UV(61f, 352f),

        // left
        UV(265f, 295f),
        UV(300f, 295f),
        UV(265f, 15f),
        UV(300f, 15f),

        // top
        UV(128f, 315f),
        UV(259f, 315f),
        UV(128f, 353f),
        UV(259f, 353f),
    };
    }

    internal GameObject CreateUnityGameObject()
    {
        var mesh = new Mesh();
        var go = new GameObject();
        var mf = go.AddComponent<MeshFilter>();
        var mr = go.AddComponent<MeshRenderer>();

        var coords = new List<Vector3>();
        var tris = new List<int>();
        var uvs = new List<Vector2>();
        var normalz = new List<Vector3>();

        for (int i = 0; i < faces.Count; i++)
        {
            var f = faces[i];
            var n = normals[i];

            coords.Add(vertices[f.x]); tris.Add(i * 3);
            coords.Add(vertices[f.y]); tris.Add(i * 3 + 1);
            coords.Add(vertices[f.z]); tris.Add(i * 3 + 2);

            uvs.Add(texture_coordinates[texture_index_list[i].x]);
            uvs.Add(texture_coordinates[texture_index_list[i].y]);
            uvs.Add(texture_coordinates[texture_index_list[i].z]);

            normalz.Add(n); normalz.Add(n); normalz.Add(n);
        }

        mesh.vertices = coords.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.normals = normalz.ToArray();
        mesh.RecalculateBounds();
        mf.mesh = mesh;

        var mat = Resources.Load<Material>("Mat_P");
        if (mat != null) mr.sharedMaterial = mat;

        return go;
    }

}