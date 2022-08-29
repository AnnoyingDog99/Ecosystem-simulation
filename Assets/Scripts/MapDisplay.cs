using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour
{

    private Mesh mesh;
    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DrawTexture(Texture2D texture)
    {
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height); //set size of the plane to the same size as the map
    }

    public void DrawMesh(MeshData meshData)
    {
        DestroyImmediate(mesh);
        mesh = meshData.CreateMesh();
        meshFilter.sharedMesh = mesh;
        meshRenderer.GetComponent<MeshCollider>().sharedMesh = meshFilter.sharedMesh;
    }

}