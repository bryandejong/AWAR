using UnityEditor;
using UnityEngine;

namespace Awar.Grid
{
    public class GridController : MonoBehaviour
    {
        public static GridController Get { get; set; }
        public AwarGrid Grid { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        [SerializeField] private Material _cellMaterial = default;
        [SerializeField] private Mesh _cellMesh = default;

        public void Initialize(int width, int height)
        {
            Width = width;
            Height = height;
            Grid = new AwarGrid(Width, Height, _cellMaterial, _cellMesh);
            Get = this;
        }

        public void Tick()
        {
            //Grid.Draw();
        }

        public static Vector3 SnapToGrid(Vector3 position)
        {
            position.x = Mathf.FloorToInt(position.x);
            position.z = Mathf.CeilToInt(position.z);

            return position;
        }

        public bool CheckIfEmpty(Vector3 origin, Vector2[] points)
        {
            Vector2 gridPosition = WorldToGridPosition(origin);
            for (int i = 0; i < points.Length; i++)
            {
                if (!Grid.IsEmpty((int) (gridPosition.x + points[i].x), (int) (gridPosition.y + points[i].y)))
                {
                    return false;
                }
            }

            return true;
        }

        public Vector2 WorldToGridPosition(Vector3 worldPosition)
        {
            Vector3 snappedPosition = SnapToGrid(worldPosition);
            Vector2 gridPosition = new Vector2(snappedPosition.x + Width / 2f, snappedPosition.z + Height / 2f);
            return gridPosition;
        }

        public void PlaceObjectOnGrid(Vector3 position, Vector2[] points)
        {
            Vector2 gridPosition = WorldToGridPosition(position);

            for (int i = 0; i < points.Length; i++)
            {
                Grid.GetCell(
                    (int) (points[i].x + gridPosition.x), 
                    (int) (points[i].y + gridPosition.y)).IsEmpty = false;
            }
        }

        /// <summary>
        /// Returns the cell at the given grid coordinates
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public GridCell GetCell(Vector2 position)
        {
            return Grid.GetCell((int)position.x, (int)position.y);
        }

        public GridCell GetCell(int x, int y)
        {
            return Grid.GetCell(x, y);
        }

        /// <summary>
        /// Returns the cell at the given world coordinates
        /// </summary>
        /// <param name="worldPos"></param>
        /// <returns></returns>
        public GridCell GetCellAtWorldPosition(Vector3 worldPos)
        {
            Vector2 gridPos = WorldToGridPosition(worldPos);
            return GetCell(gridPos);
        }
    }
}
