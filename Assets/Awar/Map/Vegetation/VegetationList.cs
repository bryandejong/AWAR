using System.Collections.Generic;
using UnityEngine;

namespace Awar.Map.Vegetation
{
    [System.Serializable]
    public class VegetationList
    {
        [Range(0f, .4f)]
        public float Density = 0.05f;
        public List<VegetationTree> Trees = default;
        public List<VegetationRock> Rocks = default;
        public List<VegetationBush> Bushes = default;

        public VegetationTree GetTree()
        {
            if (Trees.Count > 0)
            {
                return Trees[Random.Range(0, Trees.Count)];
            }

            return null;
        }

        public VegetationBush GetBush()
        {
            if (Bushes.Count > 0)
            {
                return Bushes[Random.Range(0, Bushes.Count)];
            }

            return null;
        }

        public VegetationRock GetRock()
        {
            if (Rocks.Count > 0)
            {
                return Rocks[Random.Range(0, Rocks.Count)];
            }

            return null;
        }
    }
}
