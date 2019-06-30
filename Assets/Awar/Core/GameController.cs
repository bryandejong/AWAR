using Awar.Grid;
using Awar.Map;
using UnityEngine;

namespace Awar.Core
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] public MapGenerator MapGenerator;
        [SerializeField] public GridController GridController;

        // Start is called before the first frame update
        void Awake()
        {
            GridController.Initialize(MapGenerator.Size, MapGenerator.Size);
        }

        void Update()
        {
            GridController.Tick();
        }
    }
}
