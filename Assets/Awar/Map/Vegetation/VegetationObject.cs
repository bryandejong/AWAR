using Awar.Core;
using Awar.Grid;
using Awar.Tasks;
using Awar.Village;
using UnityEngine;
using UnityEngine.UI;

namespace Awar.Map.Vegetation
{
    public class VegetationObject : AwarBehavior, IInteractable
    {
        private float _health = 5;
        public float Health
        {
            get => _health;
            set
            {
                _health = value;
                if (_health <= 0)
                    CompleteHarvest();
            }
        }

        [SerializeField] private GameObject _model = default;
        [SerializeField] private Canvas _canvas = default;

        public override void Initialize()
        {
            _model.transform.localPosition += new Vector3(Random.Range(-.2f, .2f), 0, Random.Range(-.2f, .2f));
            float randomScale = Random.Range(-.2f, .2f);
            _model.transform.localScale += new Vector3(randomScale, randomScale, randomScale);
        }

        public override void Tick()
        {
        }

        public void MarkForHarvest()
        {
            _canvas.gameObject.SetActive(true);
            VillageController.Get.MarkedVegetation.Add(this);
        }

        public void CompleteHarvest()
        {
            VillageController.Get.MarkedVegetation.Remove(this);
        }
        
        // Interactable section
        public bool Targeted { get; set; }

        public bool Targetable()
        {
            return !Targeted;
        }
    }
}
