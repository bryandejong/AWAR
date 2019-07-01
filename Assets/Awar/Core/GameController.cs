using Awar.Grid;
using Awar.Map;
using UnityEngine;

namespace Awar.Core
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] public MapGenerator MapGenerator;
        [SerializeField] public GridController GridController;

        private void Start()
        {
            Initialize();
        }

        // Start is called before the first frame update
        public void Initialize()
        {
            GridController.Initialize(MapGenerator.Size, MapGenerator.Size);
            MapGenerator.GenerateMap();
        }

        void Update()
        {
            GridController.Tick();
        }
    }
}
