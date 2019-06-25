using Awar.Grid;
using UnityEngine;

namespace Awar.Building
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField] public GameObject _placingObject = default;
        [SerializeField] private GridController _gridController = default;
        [SerializeField] private bool _buildMode = false;

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
            if (_buildMode)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = _zPos;
                Ray ray = UnityEngine.Camera.main.ScreenPointToRay(mousePos);

                if (Physics.Raycast(ray, out var hit))
                {
                    GridCell cell = _cellSelector.HoverRadius(hit.point, 3);
                    _placingObject.transform.position = cell.transform.position;
                    if (Input.GetMouseButtonDown(0))
                    {
                        Instantiate(_placingObject, cell.transform.position - new Vector3(0, .1f, 0), new Quaternion(0, 0, 0, 0));
                    }
                }
            } 
        }
    }
}
