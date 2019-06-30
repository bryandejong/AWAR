using System.Numerics;
using UnityEngine.UIElements;

namespace Awar.Grid
{
    public class GridCell
    {
        public readonly Vector2 Position;
        public bool IsEmpty = true;

        public GridCell(int x, int y)
        {
            Position = new Vector2(x, y);
        }
    }
}
