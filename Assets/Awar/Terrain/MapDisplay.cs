using System;
using Awar.Utils;
using UnityEngine;

namespace Awar.Terrain
{
    public class MapDisplay : MonoBehaviour
    {
        public Renderer Renderer;
        public MeshFilter MeshFilter;
        public MeshRenderer MeshRenderer;

        public void DrawTexture(Texture2D texture)
        {
            Renderer.sharedMaterial.mainTexture = texture;
            Renderer.transform.localScale = new Vector3(texture.width, 1, texture.height);

        }

        public void DrawMesh(MeshData mesh, Texture2D texture)
        {
            MeshFilter.sharedMesh = mesh.CreateMesh();
            MeshRenderer.sharedMaterial.mainTexture = texture;
        }
    }
}
