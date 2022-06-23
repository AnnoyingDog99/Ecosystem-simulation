﻿using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour
{

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
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.GetComponent<MeshCollider>().sharedMesh = meshFilter.sharedMesh;
    }

}