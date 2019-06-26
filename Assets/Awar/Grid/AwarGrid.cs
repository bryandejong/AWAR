using System;
using UnityEngine;

namespace Awar.Grid
{
    public class AwarGrid
    {
        public GridCell[,] Cells;

        public AwarGrid(int width, int height)
        {
            Cells = new GridCell[width, height];
        }

        public GridCell GetCell(int x, int y)
        {
            if (x < 0 || x > Cells.GetLength(0) - 1 || y < 0 || y > Cells.GetLength(1) - 1)
            {
                return null;
            }
            return Cells[x, y];
        }
    }
}
