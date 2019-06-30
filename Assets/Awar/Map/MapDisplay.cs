using Awar.Core;
using Awar.Utils;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

namespace Awar.Map
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class MapDisplay : AwarBehavior
    {
        // public Renderer Renderer;
        public MeshFilter MeshFilter;
        public MeshRenderer MeshRenderer;
        public MeshCollider MeshCollider;

        /*public void DrawTexture(Texture2D texture)
        {
            Renderer.sharedMaterial.mainTexture = texture;
            Renderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
        }*/

        public override void Initialize()
        {
            MeshFilter = GetComponent<MeshFilter>();
            MeshRenderer = GetComponent<MeshRenderer>();
            MeshCollider = GetComponent<MeshCollider>();
        }

        public override void Tick()
        {
            throw new System.NotImplementedException();
        }

        public void DrawMesh(MeshData mesh, Material material, Texture2D texture)
        {
            MeshFilter.sharedMesh = mesh.CreateMesh();
            Material newMaterial = new Material(material);
            newMaterial.SetTexture("_MainTex", texture);
            MeshRenderer.sharedMaterial = newMaterial;

#if UNITY_EDITOR
            /*
            Material src = (Material)AssetDatabase.LoadMainAssetAtPath("Assets/Materials/MaterialMap.mat");
            AssetDatabase.CreateAsset(texture, "Assets/Resources/MapTexture");
            src.mainTexture = texture;
            EditorUtility.SetDirty(src);
            MeshRenderer.sharedMaterial = src;
            */
#endif
        }


    }
}
