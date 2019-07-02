using Awar.UI;
using UnityEngine;

namespace Awar.Core
{
    public class TimeController : AwarBehavior
    {
        public static float DayTime { get; set; }
        [Range(0, 3)]
        public int TimeScale = 1;
        [Range(0.5f, 7f)]
        public float SimulationSpeed = 2f;

        [SerializeField] private GameObject _sunLight = default;
        [SerializeField] private TimeUI _ui = default;

        public override void Initialize()
        {
            DayTime = .5f;
        }

        public override void Tick()
        {
            float xRotation = SimulationSpeed * TimeScale * Time.deltaTime;
            float yRotation = SimulationSpeed / 2 * TimeScale * Time.deltaTime;
            _sunLight.transform.Rotate(xRotation, yRotation, 0);
        }

        public void SetTimeScale(int timeScale)
        {
            TimeScale = timeScale;
            _ui.UpdateUI(TimeScale);
        }
    }
}
