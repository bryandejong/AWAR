using Awar.Grid;
using Awar.Map;
using UnityEngine;

namespace Awar.Core
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] public MapGenerator MapGenerator;
        [SerializeField] public GridController GridController;
        [SerializeField] public TimeController TimeController;

        private void Start()
        {
            Initialize();
        }

        // Start is called before the first frame update
        public void Initialize()
        {
            GridController.Initialize(MapGenerator.Size, MapGenerator.Size);
            MapGenerator.GenerateMap();
            TimeController.Initialize();
        }

        void Update()
        {
            GridController.Tick();
            TimeController.Tick();
        }
    }
}
