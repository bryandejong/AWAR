using Awar.Grid;
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

        public NoiseSettings NoiseSettings;
        public Material TerrainMaterial;
        public float HeightMultiplier = 7f;
        public AnimationCurve HeightCurve;
        
        public bool AutoUpdate;
        public bool HasVegetation = true;

        public TerrainType[] Regions;

        [SerializeField] private GameObject _meshContainer;
        [SerializeField] private GameObject _vegetationContainer;
        [SerializeField] private GameObject _exampleTree = default;

        [Range(64, 320)]
        public int Size = 64;

        [Range(0, 1)]
        public float VegetationDensity = .2f;
        private int _chunkSize = 64;

        public void Awake()
        {
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
            CreateVegetationContainer();

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
                    display.MeshCollider.sharedMesh = display.MeshFilter.sharedMesh;

                    chunk.transform.parent = _meshContainer.transform;
                    chunk.transform.position = new Vector3(chunkOffsetX + _chunkSize / 2, 0, chunkOffsetY + _chunkSize / 2);

                    //Instantiate vegetation
                    if(HasVegetation) GenerateVegetation(heightMap, chunkOffsetX, chunkOffsetY);


                    chunkOffsetX += _chunkSize;
                }

                chunkOffsetY += _chunkSize;
            }
        }

        private void GenerateVegetation(float[,] heightMap, int offsetX, int offsetY)
        {
            for (int y = 2; y < heightMap.GetLength(1) - 1; y++)
            {
                for (int x = 1; x < heightMap.GetLength(0) - 1; x++)
                {
                    if (HeightCurve.Evaluate(heightMap[x, y]) > .02f) continue;

                    if (Random.Range(0, 1f) > VegetationDensity) continue;

                    Vector3 position = new Vector3(x + offsetX,
                       HeightCurve.Evaluate(heightMap[x, y]) * HeightMultiplier,
                       offsetY + _chunkSize - y);
                    GameObject spawnedTree = Instantiate(_exampleTree, position, Quaternion.identity, _vegetationContainer.transform);
                    GridController.Get.PlaceObjectOnGrid(spawnedTree.transform.position + new Vector3(0, 0, 1), new[]{new Vector2(0, 0)});
                    spawnedTree.GetComponent<VegetationObject>().Initialize();
                }
            }
        }

        private float[,] GenerateHeightMap(int chunkOffsetX, int chunkOffsetY)
        {
            Vector2 chunkOffset = new Vector2(chunkOffsetX, chunkOffsetY);
            return Noise.GenerateNoiseMap(NoiseSettings, _chunkSize + 1, chunkOffset);
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

        private void CreateVegetationContainer()
        {
            if (_vegetationContainer != null)
            {
                DestroyImmediate(_vegetationContainer);
            }

            _vegetationContainer = new GameObject("VegetationContainer");
            _vegetationContainer.transform.parent = transform;
        }

        public enum DrawMode { HeightMap, ColorMap, Mesh };

        private void OnValidate()
        {
            if (Size % _chunkSize != 0)
            {
                Size = Size - Size % _chunkSize;
            }

            Mathf.Clamp(Size, _chunkSize, 320);
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string Name;
    public float Height;
    public Color Color;
}