using UnityEngine;

namespace Awar.Building
{
    public class BuildingObject : MonoBehaviour
    {
        public Vector2 Dimensions;

        [SerializeField] private BuildingHologram _hologram = default;
        [SerializeField] private BuildingFrame _frame = default;

        public void PlaceObject()
        {
            _hologram.SetHologramMode(HologramMode.Disabled);
            _frame.Initialize();
        }
    }
}
