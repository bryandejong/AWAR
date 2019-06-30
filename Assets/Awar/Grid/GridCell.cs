using System.Numerics;
using UnityEngine.UIElements;

namespace Awar.Grid
{
    public class GridCell
    {
        public readonly Vector2 Position;

        public GridCell(int x, int y)
        {
            Position = new Vector2(x, y);
        }
    }
}
