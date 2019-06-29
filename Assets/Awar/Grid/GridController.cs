using System;
using Awar.Map;
using UnityEngine;

namespace Awar.Grid
{
    public class GridController : MonoBehaviour
    {
        [HideInInspector] int Width, Height;
        
        [SerializeField] private MapGenerator _map = default;
        [SerializeField] private GameObject _cellPrefab = default;
        private AwarGrid Grid;

        private void Start()
        {
            InitializeGrid();
        }

        public void InitializeGrid()
        {
            Width = _map.Size / 2;
            Height = _map.Size / 2;

            string gridContainerName = "GridContainer";
            Transform gridContainer = transform.Find(gridContainerName);

            if(gridContainer != null)
            {
                DestroyImmediate(gridContainer.gameObject);
            }

            gridContainer = new GameObject(gridContainerName).transform;
            gridContainer.parent = transform;
            gridContainer.position = new Vector3(0 - (_map.Size / 2), .1f,0 + (_map.Size / 2));

            Grid = GridGenerator.GenerateGrid(gridContainer, Width, Height, _cellPrefab);

        }

        /// <summary>
        /// Gets cell at the given x and y position, returns null if position is out of bounds
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public GridCell GetCell(int x, int y)
        {
            if (x < 0 || x > Width || y < 0 || y > Height)
            {
                return null;
            }
            return Grid.GetCell(x, y);
        }

        /// <summary>
        /// Gets the cell at the given world position
        /// </summary>
        /// <param name="worldPosition"></param>
        /// <returns></returns>
        public GridCell GetCell(Vector3 worldPosition)
        {
            Vector2 gridPos = WorldToGridPos(worldPosition);

            GridCell cell = GetCell((int)gridPos.x, (int)gridPos.y);
            return cell;
        }

        public Vector2 WorldToGridPos(Vector3 worldPosition)
        {
            int xPoint = Mathf.FloorToInt(worldPosition.x) + 50;
            int yPoint = (Mathf.FloorToInt(worldPosition.z) - 49) * -1;

            return new Vector2(xPoint, yPoint);
        }
    }
}
