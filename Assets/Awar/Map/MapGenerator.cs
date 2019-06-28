using Awar.Map.Vegetation;
using Awar.Utils;
using UnityEngine;

namespace Awar.Map
{
    public class MapGenerator : MonoBehaviour
    {
        public DrawMode MapDrawMode;
        public int Width, Height;
        public float NoiseScale;

        [Range(1, 8)]
        public int Octaves;
        [Range(0f, 1f)]
        public float Persistance;
        public float Lacunarity;
        public int Seed;
        public Vector2 Offset;
        public float HeightMultiplier;
        public AnimationCurve HeightCurve;

        public MapDisplay MapDisplay;
        public VegetationGenerator VegetationGenerator;

        public bool hasVegetation;

        public bool AutoUpdate;

        public TerrainType[] Regions;

        [SerializeField]
        private GameObject vegContainer;

        [ExecuteInEditMode]
        public void Awake()
        {
            GenerateMap();
        }

        public void GenerateMap()
        {
            float[,] heightMap = Noise.GenerateNoiseMap(Width, Height, Seed, NoiseScale, Octaves, Persistance, Lacunarity, Offset);
            Color[] colorMap = new Color[Width * Height];
            
            if (vegContainer != null)
            {
                DestroyImmediate(vegContainer);
            }

            vegContainer = new GameObject("Vegetation Container");

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    float currentHeight = heightMap[x, y];
                    for (int i = 0; i < Regions.Length; i++)
                    {
                        if (currentHeight <= Regions[i].Height)
                        {
                            colorMap[y * Width + x] = Regions[i].Color;
                            if (Regions[i].VegetationList.Density < Random.Range(0f, 1f)) break;

                            VegetationTree tree = Regions[i].VegetationList.GetTree();
                            if (tree == null) break;
                            tree.Initialize();
                            Instantiate(tree.gameObject,
                                new Vector3(((Width - 1) / -2f) + x, 0, ((Height - 1) / 2f) - y), Quaternion.identity, vegContainer.transform);

                            break;
                        }
                    }
                }
            }

            switch (MapDrawMode)
            {
                case(DrawMode.HeightMap):
                    MapDisplay.DrawTexture(TextureGenerator.TextureFromHeightMap(heightMap));
                    break;
                case(DrawMode.ColorMap):
                    MapDisplay.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, Width, Height));
                    break;
                case(DrawMode.Mesh):
                    MapDisplay.DrawMesh(MeshGenerator.GenerateTerrainMesh(heightMap, HeightMultiplier, HeightCurve), TextureGenerator.TextureFromColorMap(colorMap, Width, Height));
                    break;
            }
        }

        private void OnValidate()
        {
            if (Width < 1)
            {
                Width = 1;
            }

            if (Height < 1)
            {
                Height = 1;
            }

            if (Lacunarity < 1)
            {
                Lacunarity = 1;
            }

            if (Octaves < 1)
            {
                Octaves = 1;
            }

            if (NoiseScale <= 1)
            {
                NoiseScale = 1.1f;
            }
        }

        public enum DrawMode { HeightMap, ColorMap, Mesh};
    }
}

[System.Serializable]
public struct TerrainType
{
    public string Name;
    public float Height;
    public Color Color;
    public VegetationList VegetationList;
}