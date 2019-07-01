using UnityEngine;

namespace Awar.Utils
{
    [CreateAssetMenu(fileName = "New Noise Settings", menuName = "Noise/Noise Settings")]
    public class NoiseSettings : ScriptableObject
    {
        public Noise.NormalizeMode NormalizeMode;
        public Material TerrainMaterial;

        public float NoiseScale;

        [Range(1, 8)]
        public int Octaves;
        [Range(0f, 1f)]
        public float Persistance;
        public float Lacunarity;
        public int Seed;
        public Vector2 Offset;

        private void OnValidate()
        {

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
    }
}
