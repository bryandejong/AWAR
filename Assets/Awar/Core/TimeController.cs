using UnityEngine;

namespace Awar.Core
{
    public class TimeController : AwarBehavior
    {
        public static float DayTime { get; set; }
        [Range(0, 3)]
        public int SpeedMultiplier = 1;

        [Range(0.5f, 7f)]
        public float SimulationSpeed = 2f;

        [SerializeField] private GameObject _sunLight = default;

        public override void Initialize()
        {
            DayTime = .5f;
        }

        public override void Tick()
        {
            float xRotation = SimulationSpeed * SpeedMultiplier * Time.deltaTime;
            float yRotation = SimulationSpeed / 2 * SpeedMultiplier * Time.deltaTime;
            _sunLight.transform.Rotate(xRotation, yRotation, 0);
        }
    }
}
