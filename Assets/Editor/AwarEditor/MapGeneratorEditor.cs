﻿using Awar.Map;
using UnityEditor;
using UnityEngine;

namespace AwarEditor
{
    [CustomEditor(typeof(MapGenerator))]
    public class MapGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            MapGenerator mapGenerator = (MapGenerator) target;

            if (DrawDefaultInspector())
            {
                if (mapGenerator.AutoUpdate)
                {
                    mapGenerator.GenerateMap();
                }
            }

            if (GUILayout.Button("Generate"))
            {
                mapGenerator.GenerateMap();
            }
        }
    }
}
