using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    Color32[] colors;

    public int xSize = 20;
    public int zSize = 20;
    public float xOffset = .3f;
    public float zOffset = .3f;

    public Gradient gradient;

    float minTerrainHeight;
    float maxTerrainHeight;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape(){
        vertices = new Vector3[(xSize * 6) * (zSize * 6)];
        int tri = 0;
        float y;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                y = Mathf.PerlinNoise((0 + x) * xOffset, (0 + z) * zOffset) * 2f;
                vertices[tri] = new Vector3(0 + x, y, 0 + z);
                SetMinMaxHeight(y);
                y = Mathf.PerlinNoise((0 + x) * xOffset, (1 + z) * zOffset) * 2f;
                vertices[1 + tri] = new Vector3(0 + x, y, 1 + z);
                SetMinMaxHeight(y);
                y = Mathf.PerlinNoise((1 + x) * xOffset, (0 + z) * zOffset) * 2f;
                vertices[2 + tri] = new Vector3(1 + x, y, 0 + z);
                SetMinMaxHeight(y);

                y = Mathf.PerlinNoise((1 + x) * xOffset, (1 + z) * zOffset) * 2f;
                vertices[3 + tri] = new Vector3(1 + x, y, 1 + z);
                SetMinMaxHeight(y);
                y = Mathf.PerlinNoise((1 + x) * xOffset, (0 + z) * zOffset) * 2f;
                vertices[4 + tri] = new Vector3(1 + x, y, 0 + z);
                SetMinMaxHeight(y);
                y = Mathf.PerlinNoise((0 + x) * xOffset, (1 + z) * zOffset) * 2f;
                vertices[5 + tri] = new Vector3(0 + x, y, 1 + z);
                SetMinMaxHeight(y);
                
                tri += 6;
            }

        }
        triangles = new int[vertices.Length];
        for (int i = 0; i < triangles.Length; i++)
        {
            triangles[i] = i;
        }
        colors = new Color32[vertices.Length];
        for(int i = 0, z = 0; z < zSize; z++){
            for(int x = 0; x < xSize; x++){
                float height1 = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, vertices[i].y);
                colors[i] = gradient.Evaluate(height1);
                colors[i + 1] = gradient.Evaluate(height1);
                colors[i + 2] = gradient.Evaluate(height1);
                float height2 = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, vertices[i + 3].y);
                colors[i + 3] = gradient.Evaluate(height2);
                colors[i + 4] = gradient.Evaluate(height2);
                colors[i + 5] = gradient.Evaluate(height2);
                i += 6;
            }
        }
    }
    void UpdateMesh(){
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors32 = colors;

        mesh.RecalculateNormals();
    }
    // private void OnDrawGizmos(){
    //     if(vertices == null){
    //         return;
    //     }
    //     for (int i = 0; i < vertices.Length; i++)
    //     {
    //         Gizmos.DrawSphere(vertices[i], .1f);
    //     }
    // }

    void SetMinMaxHeight(float y){
        if (y > maxTerrainHeight){
            maxTerrainHeight = y;
        }
        if(y < minTerrainHeight){
            minTerrainHeight = y;
        }
    }
}
