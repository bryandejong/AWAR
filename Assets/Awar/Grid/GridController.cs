using UnityEngine;

namespace Awar.Grid
{
    public class GridController : MonoBehaviour
    {
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
        }

        public void Tick()
        {
            Grid.Draw();
        }
    }
}
