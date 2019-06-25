using Boo.Lang.Environments;
using UnityEngine;

namespace Awar.Grid
{
    public static class GridGenerator
    {
        public static AwarGrid GenerateGrid(Transform parent, int width, int height, GameObject cellPrefab)
        {
            AwarGrid grid = new AwarGrid(width, height);

            for (int y = 0; y < height; y++)
            {
                Transform row = new GameObject($"Row {y}").transform;
                row.parent = parent;
                row.position = new Vector3(parent.position.x, parent.position.y, parent.position.z - y);
                for (int x = 0; x < width; x++)
                {
                    GameObject cell = Object.Instantiate(cellPrefab, new Vector3(row.position.x + x, row.position.y, row.position.z), Quaternion.Euler(90, 0, 0), row);
                    grid.Cells[x, y] = cell.GetComponent<GridCell>();
                }
            }

            return grid;
        }
    }
}
