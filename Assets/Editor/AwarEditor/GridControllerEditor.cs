using Awar.Grid;
using UnityEditor;
using UnityEngine;

namespace AwarEditor
{
    [CustomEditor(typeof(GridController))]
    public class GridControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GridController gridController = (GridController) target;

            DrawDefaultInspector();

            if (GUILayout.Button("Initialize Grid"))
            {
                gridController.InitializeGrid();
            }
        }
    }
}
