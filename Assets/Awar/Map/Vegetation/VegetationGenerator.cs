using UnityEngine;

namespace Awar.Map.Vegetation
{
    public class VegetationGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject vegetationContainer;

        public void GenerateVegetation(float[,] heightMap, float multiplier, AnimationCurve heightCurve, TerrainType[] regions)
        {
            if (vegetationContainer != null)
            {
                DestroyImmediate(vegetationContainer);
            }

            vegetationContainer = new GameObject("Vegetation Container");

            int width = heightMap.GetLength(0);
            int height = heightMap.GetLength(1);
        }
    }
}
