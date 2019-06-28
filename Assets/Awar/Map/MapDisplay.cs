using Awar.Utils;
using UnityEditor;
using UnityEngine;

namespace Awar.Map
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
            MeshRenderer.sharedMaterial.SetTexture("_MainTex", texture);

#if UNITY_EDITOR

            Material src = (Material)AssetDatabase.LoadMainAssetAtPath("Assets/Materials/MaterialMap.mat");
            AssetDatabase.CreateAsset(texture, "Assets/Resources/MapTexture");
            src.mainTexture = texture;
            EditorUtility.SetDirty(src);
            MeshRenderer.sharedMaterial = src;

#endif

        }
    }
}
