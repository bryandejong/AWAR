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
                if (!Grid.IsEmpty((int) (points[i].x + gridPosition.x), (int) (points[i].y + gridPosition.y)))
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
    }
}
