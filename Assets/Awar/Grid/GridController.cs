using Awar.Terrain;
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
            Width = _map.Width - 1;
            Height = _map.Height - 1;

            string gridContainerName = "GridContainer";
            Transform gridContainer = transform.Find(gridContainerName);

            if(gridContainer != null)
            {
                DestroyImmediate(gridContainer.gameObject);
            }

            gridContainer = new GameObject(gridContainerName).transform;
            gridContainer.parent = transform;
            gridContainer.position = new Vector3(0 - (_map.Width / 2) + .5f, .1f,0 + (_map.Height / 2) - .5f);

            Grid = GridGenerator.GenerateGrid(gridContainer, Width, Height, _cellPrefab);

        }
    }
}
