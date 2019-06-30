using Awar.Grid;
using Awar.Utils;
using Awar.Village;
using UnityEngine;

namespace Awar.Construction
{
    public class ConstructionController : MonoBehaviour
    {
        [SerializeField] private bool _buildMode = false;

        private ConstructionObject _constructionObject;
        private GameObject _placingGameObject;

        public void SetBuilding(ConstructionObject construction)
        {
            _placingGameObject = Instantiate(construction.gameObject);
            _constructionObject = _placingGameObject.GetComponent<ConstructionObject>();
            _buildMode = true;
        }

        private void Update()
        {
            if (_buildMode)
            {
                if (HasCancelledBuildMode()) { return; }

                Ray ray = MouseControl.MouseRay();

                if (Physics.Raycast(ray, out var hit))
                {
                    _placingGameObject.transform.position = GridController.SnapToGrid(hit.point);
                    bool isValidPosition = GridController.Get.CheckIfEmpty(_placingGameObject.transform.position, _constructionObject.Shape);

                    if (isValidPosition == false)
                    {
                        _constructionObject.SetHologramMode(HologramMode.Invalid);
                    }
                    else
                    {
                        _constructionObject.SetHologramMode(HologramMode.Valid);
                    }

                    if (Input.GetMouseButtonDown(0) && isValidPosition)
                    {
                        PlaceBuilding();
                    }
                }
            }
        }

        private void PlaceBuilding()
        {
            GameObject placedObject = Instantiate(_placingGameObject, _placingGameObject.transform.position, _placingGameObject.transform.rotation);
            //Can possibly be refactored into the ConstructionObject's start method
            ConstructionObject constructionObject = placedObject.GetComponent<ConstructionObject>();
            constructionObject.PlaceObject();
            //

            GridController.Get.PlaceObjectOnGrid(constructionObject.transform.position, constructionObject.Shape);
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
            Destroy(_placingGameObject);
        }
    }
}
