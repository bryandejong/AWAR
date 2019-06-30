
using UnityEngine;
using UnityEngine.Rendering;

namespace Awar.Grid
{
    public class AwarGrid
    {
        private GridCell[,] _cells;
        private Matrix4x4[][] _cellMatrices;
        private Material _cellMaterial;
        private Mesh _cellMesh;

        public AwarGrid(int width, int height, Material cellMaterial, Mesh cellMesh)
        {
            InitializeGrid(width, height);
            _cellMaterial = cellMaterial;
            _cellMesh = cellMesh;
        }

        private void InitializeGrid(int width, int height)
        {
            _cells = new GridCell[width, height];
            int totalSize = width * height;
            int totalBatches = Mathf.CeilToInt(totalSize / 1023f);
            _cellMatrices = new Matrix4x4[totalBatches][];
            for (int i = 0; i < totalBatches; i++)
            {
                int batchSize = i < totalBatches - 1 ? 1023 : totalSize % 1023;
                _cellMatrices[i] = new Matrix4x4[batchSize];
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    _cells[x, y] = new GridCell(x, y);
                    _cellMatrices[Mathf.FloorToInt((y * width + x) / 1023f)][(y * width + x) % 1023].SetTRS(new Vector3(x - width / 2 + .5f, .1f, y - height / 2 + .5f), Quaternion.Euler(90, 0, 0), Vector3.one);
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < _cellMatrices.GetLength(0); i++)
            {
                int instancesInBatch = Mathf.NextPowerOfTwo(_cellMatrices[i].Length) - 1;

                if (instancesInBatch < _cellMatrices[i].Length || instancesInBatch > _cellMatrices[i].Length)
                {
                    instancesInBatch = _cellMatrices[i].Length;
                }


                Graphics.DrawMeshInstanced(_cellMesh, 0,
                    _cellMaterial,
                    _cellMatrices[i],
                    instancesInBatch,
                    null,
                    ShadowCastingMode.Off,
                    false,
                    0, default, LightProbeUsage.Off, null);
            }
        }

        public bool IsEmpty(int x, int y)
        {
            return _cells[x, y].IsEmpty;
        }

        public GridCell GetCell(int x, int y)
        {
            Debug.Log($"{x},{y}");
            return _cells[x, y];
        }
    }
}
