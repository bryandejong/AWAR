using Awar.Grid;
using Awar.Utils;
using Awar.Village;
using UnityEngine;

namespace Awar.Construction
{
    public class ConstructionController : MonoBehaviour
    {
        [SerializeField] private GridController _gridController = default;
        [SerializeField] private bool _buildMode = false;

        private GridCellSelector _cellSelector = default;

        private ConstructionObject _placingTemplate;
        private GameObject _placingObject;

        public void SetBuilding(ConstructionObject construction)
        {
            _placingTemplate = construction;
            _placingObject = Instantiate(_placingTemplate.gameObject);
            _buildMode = true;
        }

        private void Start()
        {
            _cellSelector = new GridCellSelector(_gridController);
        }

        private void Update()
        {
            if (_buildMode)
            {
                if (HasCancelledBuildMode()) { return; }

                Ray ray = MouseControl.MouseRay();

                if (Physics.Raycast(ray, out var hit))
                {
                    GridCell cell = _cellSelector.HoverRadius(hit.point, 3, (int)_placingTemplate.Dimensions.x, (int)_placingTemplate.Dimensions.y);
                    _placingObject.transform.position = new Vector3(cell.transform.position.x, 0.01f, cell.transform.position.z);
                    if (Input.GetMouseButtonDown(0))
                    {
                        PlaceBuilding();
                    }
                }
            }
        }

        private void PlaceBuilding()
        {
            GameObject placedObject = Instantiate(_placingObject, _placingObject.transform.position, _placingObject.transform.rotation);
            ConstructionObject constructionObject = placedObject.GetComponent<ConstructionObject>();
            constructionObject.PlaceObject();

            CancelBuildMode();   
        }

        /// <summary>
        /// Checks if user pressed a cancel button, and handles cancel behavior
        /// </summary>
        /// <returns>True when user cancelled build mode</returns>
        private bool HasCancelledBuildMode()
        {
            if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Escape))
            {
                CancelBuildMode();
                return true;
            }

            return false;
        }

        private void CancelBuildMode()
        {
            _buildMode = false;
            Destroy(_placingObject);
            _cellSelector.RemoveSelection();
        }
    }
}
