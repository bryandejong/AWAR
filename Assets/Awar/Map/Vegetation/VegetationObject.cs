using Awar.Core;
using UnityEngine;

namespace Awar.Map.Vegetation
{
    public class VegetationObject : AwarBehavior
    {
        [SerializeField] private GameObject _model = default;

        public override void Initialize()
        {
            _model.transform.localPosition = new Vector3(Random.Range(-.3f, .3f), 0, Random.Range(-.3f, .3f));
        }

        public override void Tick()
        {
        }
    }
}
