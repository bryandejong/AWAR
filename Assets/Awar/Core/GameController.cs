using Awar.Construction;
using Awar.Grid;
using Awar.Map;
using Awar.Village;
using UnityEngine;

namespace Awar.Core
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] public MapGenerator MapGenerator;
        [SerializeField] public GridController GridController;
        [SerializeField] public TimeController TimeController;
        [SerializeField] public ConstructionController ConstructionController;
        [SerializeField] public AreaOrderController AreaOrderController;
        [SerializeField] public VillageController VillageController;

        private void Awake()
        {
            Initialize();
        }

        // Start is called before the first frame update
        public void Initialize()
        {
            GridController.Initialize(MapGenerator.Size, MapGenerator.Size);
            MapGenerator.GenerateMap();
            VillageController.Initialize();
            TimeController.Initialize();
            ConstructionController.Initialize();
            AreaOrderController.Initialize();
        }

        void Update()
        {
            GridController.Tick();
            TimeController.Tick();
            ConstructionController.Tick();
            AreaOrderController.Tick();
        }
    }
}
