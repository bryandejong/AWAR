using Awar.Core;
using UnityEngine;

namespace Awar.Map.Vegetation
{
    public class VegetationObject : AwarBehavior
    {
        [SerializeField] private GameObject _model = default;

        public override void Initialize()
        {
            _model.transform.localPosition += new Vector3(Random.Range(-.2f, .2f), 0, Random.Range(-.2f, .2f));
            float randomScale = Random.Range(-.2f, .2f);
            _model.transform.localScale += new Vector3(randomScale, randomScale, randomScale);
        }

        public override void Tick()
        {
        }
    }
}
