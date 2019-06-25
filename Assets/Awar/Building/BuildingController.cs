using Awar.Grid;
using UnityEngine;

namespace Awar.Building
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField] public GameObject _placingObject = default;
        [SerializeField] private GridController _gridController = default;

        private GridCellSelector _cellSelector;
        private float _zPos;

        void Start()
        {
            _cellSelector = new GridCellSelector(_gridController);
            _zPos = UnityEngine.Camera.main.nearClipPlane;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = _zPos;
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _cellSelector.Hover(hit.point);
            }
        }
    }
}
