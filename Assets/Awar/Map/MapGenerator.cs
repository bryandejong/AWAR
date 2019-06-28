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

        public bool AutoUpdate;

        public TerrainType[] Regions;

        [ExecuteInEditMode]
        public void Start()
        {
            GenerateMap();
            Debug.Log("Generated");
        }

        public void GenerateMap()
        {
            float[,] noiseMap = Noise.GenerateNoiseMap(Width, Height, Seed, NoiseScale, Octaves, Persistance, Lacunarity, Offset);
            Color[] colorMap = new Color[Width * Height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    float currentHeight = noiseMap[x, y];
                    for (int i = 0; i < Regions.Length; i++)
                    {
                        if (currentHeight <= Regions[i].Height)
                        {
                            colorMap[y * Width + x] = Regions[i].Color;
                            break;
                        }
                    }
                }
            }

            switch (MapDrawMode)
            {
                case(DrawMode.HeightMap):
                    MapDisplay.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
                    break;
                case(DrawMode.ColorMap):
                    MapDisplay.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, Width, Height));
                    break;
                case(DrawMode.Mesh):
                    MapDisplay.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, HeightMultiplier, HeightCurve), TextureGenerator.TextureFromColorMap(colorMap, Width, Height));
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
}