using Awar.Map.Vegetation;
using Awar.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Awar.Map
{
    public class MapGenerator : MonoBehaviour
    {
        public DrawMode MapDrawMode;

        public Noise.NormalizeMode NormalizeMode;
        public Material TerrainMaterial;

        [Range(64, 320)]
        public int Size;
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

        public bool AutoUpdate;

        public TerrainType[] Regions;

        [SerializeField] private GameObject _meshContainer;
        private int _chunkSize = 64;

        public void Awake()
        {
            GenerateMap();
        }

        public void GenerateMap()
        {
            switch (MapDrawMode)
            {
                case (DrawMode.HeightMap):
                    Debug.LogWarning("Heightmap is currently not supported");
                    //MapDisplay.DrawTexture(TextureGenerator.TextureFromHeightMap(heightMap));
                    break;
                case (DrawMode.ColorMap):
                    Debug.LogWarning("ColorMap is currently not supported");
                    //MapDisplay.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, Width, Height));
                    break;
                case (DrawMode.Mesh):
                    GenerateChunks();
                    break;
            }
        }

        /// <summary>
        /// Generates the terrain chunks
        /// </summary>
        private void GenerateChunks()
        {
            CreateMeshContainer();

            //Determine amount of chunks
            int chunks = Size / _chunkSize;

            int chunkOffsetY = 0 - Size / 2;

            //Create chunks
            for (int y = 0; y < chunks; y++)
            {
                int chunkOffsetX = 0 - Size / 2;
                for (int x = 0; x < chunks; x++)
                {
                    float[,] heightMap = GenerateHeightMap(chunkOffsetX, chunkOffsetY);
                    Color[] colorMap = GenerateColorMap(heightMap);
                        
                    GameObject chunk = new GameObject($"Chunk [{x},{y}]");
                    MapDisplay display = chunk.AddComponent<MapDisplay>();
                    display.Initialize();
                    display.DrawMesh(
                        MeshGenerator.GenerateTerrainMesh(heightMap, HeightMultiplier, HeightCurve), 
                        TerrainMaterial,
                        TextureGenerator.TextureFromColorMap(colorMap, _chunkSize, _chunkSize));
                    chunk.transform.parent = _meshContainer.transform;
                    chunk.transform.position = new Vector3(chunkOffsetX + _chunkSize / 2, 0, chunkOffsetY + _chunkSize / 2);

                    chunkOffsetX += _chunkSize;
                }

                chunkOffsetY += _chunkSize;
            }
        }

        private float[,] GenerateHeightMap(int chunkOffsetX, int chunkOffsetY)
        {
            Vector2 chunkOffset = new Vector2(chunkOffsetX, chunkOffsetY);
            return Noise.GenerateNoiseMap(
                _chunkSize + 1, _chunkSize + 1,
                Seed, NoiseScale, Octaves, Persistance, Lacunarity,
                Offset + chunkOffset,
                NormalizeMode);
        }

        private Color[] GenerateColorMap(float[,] heightMap)
        {
            Color[] colorMap = new Color[_chunkSize * _chunkSize];

            for (int y = 0; y < _chunkSize; y++)
            {
                for (int x = 0; x < _chunkSize; x++)
                {
                    float currentHeight = heightMap[x, y];
                    for (int i = 0; i < Regions.Length; i++)
                    {
                        if (currentHeight <= Regions[i].Height)
                        {
                            colorMap[y * _chunkSize + x] = Regions[i].Color;
                            break;
                        }
                    }
                }
            }

            return colorMap;
        }

        private void CreateMeshContainer()
        {
            if (_meshContainer != null)
            {
                DestroyImmediate(_meshContainer);
            }

            _meshContainer = new GameObject("MeshContainer");
            _meshContainer.transform.parent = transform;
        }

        private void OnValidate()
        {
            if (Size % _chunkSize != 0)
            {
                Size = Size - Size % _chunkSize;
            }

            Mathf.Clamp(Size, 64, 320);

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

        public enum DrawMode { HeightMap, ColorMap, Mesh };
    }
}

[System.Serializable]
public struct TerrainType
{
    public string Name;
    public float Height;
    public Color Color;
}