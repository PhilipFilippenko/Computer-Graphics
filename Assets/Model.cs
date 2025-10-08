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
        //--- Frontside ---
        UV(25f, 295f),  // 0
        UV(62f, 295f),  // 1
        UV(62f, 155f),  // 2
        UV(155f, 155f), // 3
        UV(172f, 138f), // 4
        UV(172f, 33f),  // 5
        UV(155f, 15f),  // 6
        UV(25f, 15f),   // 7
        UV(61f, 51f),   // 8
        UV(135f, 51f),  // 9
        UV(135f, 120f), // 10
        UV(61f, 120f),  // 11

        //--- Backside ---
        UV(493f, 294f), // 12
        UV(457f, 294f), // 13
        UV(457f, 153f), // 14
        UV(365f, 153f), // 15
        UV(350f, 140f), // 16
        UV(350f, 30f),  // 17
        UV(365f, 14f),  // 18
        UV(493f, 14f),  // 19
        UV(456f, 50f),  // 20
        UV(385f, 50f),  // 21
        UV(385f, 120f), // 22
        UV(455f, 120f), // 23

        //--- Left ---
        //change it later!!!!
        UV(79, 241), // 36
        UV(230, 241), // 37
        UV(79, 264), // 38
        UV(240, 265), // 39
        
        //--- Top ---
        UV(79, 241), // 36
        UV(230, 241), // 37
        UV(79, 264), // 38
        UV(240, 265), // 39
        
        //--- Bottom ---
        UV(79, 194), // 32
        UV(180, 194), // 33
        UV(79, 224), // 34
        UV(180, 224), // 35

        UV(180, 224), // 40
        UV(180, 224), // 41
        UV(180, 224), // 42
        UV(180, 224), // 43

        //--- Right ---
        UV(193, 38), // 24
        UV(193, 24), // 25
        UV(230, 24), // 26
        UV(230, 38), // 27

        UV(193, 62), // 28
        UV(230, 62), // 29
        UV(193, 167), // 30
        UV(230, 167), // 31
        
        UV(193, 38), // 24
        UV(193, 24), // 25
        UV(230, 24), // 26
        UV(230, 38), // 27

        //--- Inside ---
        UV(79, 194), // 32
        UV(180, 194), // 33
        UV(79, 224), // 34
        UV(180, 224), // 35

        UV(79, 194), // 32
        UV(180, 194), // 33
        UV(79, 224), // 34
        UV(180, 224), // 35

        UV(79, 194), // 32
        UV(180, 194), // 33
        UV(79, 224), // 34
        UV(180, 224), // 35

        UV(79, 194), // 32
        UV(180, 194), // 33
        UV(79, 224), // 34
        UV(180, 224), // 35
    };
    }

    internal GameObject CreateUnityGameObject()
    {
        Mesh mesh = new Mesh();
        GameObject newGO = new GameObject();

        MeshFilter mesh_filter = newGO.AddComponent<MeshFilter>();
        MeshRenderer mesh_renderer = newGO.AddComponent<MeshRenderer>();

        List<Vector3> coords = new List<Vector3>();
        List<int> dummy_indices = new List<int>();
        List<Vector2> text_coords = new List<Vector2>();
        List<Vector3> normalz = new List<Vector3>();
        List<Vector2> uvCoords = new List<Vector2>();

        for (int i = 0; i < faces.Count; i++)
        {
            Vector3 normal_for_face = normals[i];

            normal_for_face = new Vector3(normal_for_face.x, normal_for_face.y, -normal_for_face.z);

            coords.Add(vertices[faces[i].x]); dummy_indices.Add(i * 3); 
            coords.Add(vertices[faces[i].y]); dummy_indices.Add(i * 3 + 1); 
            coords.Add(vertices[faces[i].z]); dummy_indices.Add(i * 3 + 2); 

            text_coords.Add(texture_coordinates[texture_index_list[i].x]); 
            text_coords.Add(texture_coordinates[texture_index_list[i].y]); 
            text_coords.Add(texture_coordinates[texture_index_list[i].z]); 

            uvCoords.Add(texture_coordinates[faces[i].x]);
            uvCoords.Add(texture_coordinates[faces[i].y]);
            uvCoords.Add(texture_coordinates[faces[i].z]);
        }

        mesh.vertices = coords.ToArray();
        mesh.triangles = dummy_indices.ToArray();
        mesh.uv = text_coords.ToArray();
        mesh.normals = normalz.ToArray();
        mesh.RecalculateNormals();
        mesh_filter.mesh = mesh;

        var mat = new Material(Shader.Find("Standard"));
        var tex = Resources.Load<Texture2D>("texture");
        if (tex != null) mat.mainTexture = tex;
        mesh_renderer.sharedMaterial = mat;

        return newGO;
    }
}