using Awar.Core;
using Awar.Grid;
using UnityEditor;
using UnityEngine;
using GameController = Awar.Core.GameController;

namespace AwarEditor
{
    [CustomEditor(typeof(GameController))]
    public class GameControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("(Re)Initialize"))
            {
                GameController controller = target as GameController;

                GridController.Get = controller?.GridController;
                controller?.Initialize();
            }
        }
    }
}