using UnityEngine;

namespace Awar.Grid
{
    public class AwarGrid
    {
        public GridCell[,] Cells;

        public AwarGrid(int width, int height)
        {
            Cells = new GridCell[width,height];
        }

        public GridCell GetCell(int x, int y)
        {
            return Cells[x, y];
        }
    }
}
