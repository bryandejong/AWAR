using UnityEngine;

namespace Awar.Utils
{
    public static class MeshGenerator
    {
        public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve)
        {
            int width = heightMap.GetLength(0);
            int height = heightMap.GetLength(1);
            float topLeftX = (width - 1) / -2f;
            float topLeftZ = (height - 1) / 2f;

            MeshData meshData = new MeshData(width, height);
            int vertexIndex = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    meshData.Vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier, topLeftZ - y);
                    meshData.UVs[vertexIndex] = new Vector2(x / (float) width, y / (float) height);

                    if (x < width - 1 && y < height - 1)
                    {
                        meshData.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
                        meshData.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                    }
                    vertexIndex++;
                }
            }

            

            return meshData;
        }

    }

    public struct MeshData
    {
        public Vector3[] Vertices;
        public Vector2[] UVs;
        public int[] Triangles;

        private int _triangleIndex;

        public MeshData(int meshWidth, int meshHeight)
        {
            Vertices = new Vector3[meshWidth * meshHeight];
            UVs = new Vector2[meshWidth * meshHeight];
            Triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
            _triangleIndex = 0;
        }

        public void AddTriangle(int a, int b, int c)
        {
            Triangles[_triangleIndex] = a;
            Triangles[_triangleIndex + 1] = b;
            Triangles[_triangleIndex + 2] = c;
            _triangleIndex += 3;
        }

        public Mesh CreateMesh()
        {
            FlatShading();

            Mesh mesh = new Mesh()
            {
                vertices = Vertices,
                triangles = Triangles,
                uv = UVs
            };

            mesh.RecalculateNormals();
            return mesh;
        }
        private void FlatShading()
        {
            Vector3[] flatShadedVertices = new Vector3[Triangles.Length];
            Vector2[] flatShadedUvs = new Vector2[Triangles.Length];

            for (int i = 0; i < Triangles.Length; i++)
            {
                flatShadedVertices[i] = Vertices[Triangles[i]];
                flatShadedUvs[i] = UVs[Triangles[i]];
                Triangles[i] = i;
            }

            Vertices = flatShadedVertices;
            UVs = flatShadedUvs;
        }

    }
}