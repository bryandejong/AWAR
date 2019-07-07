using System.Numerics;
using Awar.Map.Vegetation;
using UnityEngine.UIElements;

namespace Awar.Grid
{
    public class GridCell
    {
        private bool _isEmpty = true;

        public readonly Vector2 Position;

        public bool IsEmpty
        {
            get => Vegetation == null && _isEmpty;
            set => _isEmpty = value;
        }

        public VegetationObject Vegetation;

        public GridCell(int x, int y)
        {
            Position = new Vector2(x, y);
        }
    }
}
