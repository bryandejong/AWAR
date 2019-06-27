using Awar.AI;
using Awar.Village;
using UnityEngine;

namespace Awar.Construction
{
    public class ConstructionObject : MonoBehaviour
    {
        public Vector2 Dimensions;

        [SerializeField] private VillageObject _villageObject = default;
        [SerializeField] private ConstructionFrame _frame = default;
        [SerializeField] private ConstructionHologram _hologram = default;
        [SerializeField] private float _buildEffort = 10f;

        public void PlaceObject()
        {
            _hologram.SetHologramMode(HologramMode.Disabled);
            Destroy(_hologram.gameObject);
            VillageController.Get.ConstructionBuildings.Add(this);
            _frame.Initialize();
        }

        public void AddEffort(float amount)
        {
            if (_frame.AddProgress(amount / _buildEffort))
            {
                VillageController.Get.ConstructionBuildings.Remove(this);
                _villageObject.Initialize();
                Destroy(_frame.gameObject);
                Destroy(this);
            }
        }

        public TargetPosition GetEmptyPosition()
        {
            return _frame.GetEmptyPosition();
        }
    }
}
