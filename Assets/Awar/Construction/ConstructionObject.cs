using UnityEditor;
using UnityEngine;

namespace Awar.Construction
{
    public class ConstructionObject : MonoBehaviour
    {
        public Vector2 Dimensions;

        [SerializeField] private ConstructionHologram _hologram = default;
        [SerializeField] private ConstructionFrame _frame = default;
        [SerializeField] private float _buildEffort = 10f;

        public void PlaceObject()
        {
            _hologram.SetHologramMode(HologramMode.Disabled);
            _frame.Initialize();
        }

        public void AddEffort(float amount)
        {
            _frame.AddProgress(amount / _buildEffort);
        }
    }

    [CustomEditor(typeof(ConstructionObject))]
    public class BuildingObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            ConstructionObject target = this.target as ConstructionObject;
            DrawDefaultInspector();

            if (GUILayout.Button("Add 2 effort"))
            {
                target?.AddEffort(2);
            }
        }
    }
}
