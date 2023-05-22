using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lens : MonoBehaviour
{
    public float rad;
    // Start is called before the first frame update
    void Start()
    {
        
        return;
        Mesh mesh = new Mesh();

        Vector3[] vertices;
        Vector2[] uvs;
        int[] triangles;

        float[] rand = {10,8,6,4};

        rad = 10;

        //GenerateFresnelLens2(10, 5, 100, out vertices, out uvs, out triangles);
        GenerateMultiLevelFresnelLens(rand, rand, 10, out vertices, out uvs, out triangles);
        Debug.Log(vertices[0]);
        Debug.Log(vertices[1]);
        Debug.Log(vertices[2]);
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        GetComponent<MeshFilter>().mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

public static void GenerateFresnelLens2(float outerRadius, float innerRadius, int segments, out Vector3[] vertices, out Vector2[] uvs, out int[] triangles)
{
    vertices = new Vector3[(segments + 1) * 2];
    uvs = new Vector2[(segments + 1) * 2];
    triangles = new int[segments * 6];

    float angleDelta = 2 * Mathf.PI / segments;
    float angle = 0;

    // Outer vertices
    for (int i = 0; i <= segments; i++)
    {
        float x = Mathf.Cos(angle) * outerRadius;
        float y = Mathf.Sin(angle) * outerRadius;

        vertices[i] = new Vector3(x, y, 0);
        uvs[i] = new Vector2(x / outerRadius / 2 + 0.5f, y / outerRadius / 2 + 0.5f);

        angle += angleDelta;
    }

    // Inner vertices
    for (int i = 0; i <= segments; i++)
    {
        float x = Mathf.Cos(angle) * innerRadius;
        float y = Mathf.Sin(angle) * innerRadius;

        vertices[i + segments + 1] = new Vector3(x, y, 0);
        uvs[i + segments + 1] = new Vector2(x / innerRadius / 2 + 0.5f, y / innerRadius / 2 + 0.5f);

        angle -= angleDelta;
    }

    // Triangles
    for (int i = 0; i < segments; i++)
    {
        int index = i * 6;
        triangles[index] = i;
        triangles[index + 1] = i + segments + 1;
        triangles[index + 2] = i + 1;

        triangles[index + 3] = i + 1;
        triangles[index + 4] = i + segments + 1;
        triangles[index + 5] = i + segments + 2 > segments * 2 ? segments + 1 : i + segments + 2;
    }
}


public static void GenerateMultiLevelFresnelLens(float[] levels, float[] widths, int segments,
    out Vector3[] vertices, out Vector2[] uvs, out int[] triangles)
{
    int numLevels = levels.Length;
    int numVertsPerLevel = segments + 1;
    int numVerts = numLevels * numVertsPerLevel * 2;
    int numTrisPerLevel = segments * 2;
    int numTris = numLevels * numTrisPerLevel;

    vertices = new Vector3[numVerts];
    uvs = new Vector2[numVerts];
    triangles = new int[numTris * 3];

    // Generate vertices and UVs for each level
    for (int levelIndex = 0; levelIndex < numLevels; levelIndex++)
    {
        float levelRadius = levels[levelIndex];
        float levelWidth = widths[levelIndex];

        int baseIndex = levelIndex * numVertsPerLevel * 2;

        float angleDelta = 2 * Mathf.PI / segments;
        float angle = 0;

        // Generate outer vertices
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Cos(angle) * (levelRadius + levelWidth);
            float y = Mathf.Sin(angle) * (levelRadius + levelWidth);

            vertices[baseIndex + i] = new Vector3(x, y, -10);
            uvs[baseIndex + i] = new Vector2(x / (levelRadius + levelWidth) / 2 + 0.5f, y / (levelRadius + levelWidth) / 2 + 0.5f);

            angle += angleDelta;
        }

        // Generate inner vertices
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Cos(angle) * levelRadius;
            float y = Mathf.Sin(angle) * levelRadius;

            vertices[baseIndex + i + numVertsPerLevel] = new Vector3(x, y, 0);
            uvs[baseIndex + i + numVertsPerLevel] = new Vector2(x / levelRadius / 2 + 0.5f, y / levelRadius / 2 + 0.5f);

            angle -= angleDelta;
        }

        // Generate triangles
        int triBaseIndex = levelIndex * numTrisPerLevel * 3;
        int outerBaseIndex = baseIndex;
        int innerBaseIndex = baseIndex + numVertsPerLevel;

        for (int i = 0; i < segments; i++)
        {
            int triIndex = i * 6;
            int outerIndex = outerBaseIndex + i;
            int innerIndex = innerBaseIndex + i;

            // First triangle
            triangles[triBaseIndex + triIndex] = outerIndex;
            triangles[triBaseIndex + triIndex + 1] = innerIndex;
            triangles[triBaseIndex + triIndex + 2] = outerIndex + 1;

            // Second triangle
            triangles[triBaseIndex + triIndex + 3] = outerIndex + 1;
            triangles[triBaseIndex + triIndex + 4] = innerIndex;
            triangles[triBaseIndex + triIndex + 5] = innerIndex + 1 > innerBaseIndex + numVertsPerLevel ? innerBaseIndex : innerIndex + 1;
        }
    }

    // Reverse the order of the normals to



}
}
