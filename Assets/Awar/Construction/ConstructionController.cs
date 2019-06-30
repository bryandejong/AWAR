using Awar.Utils;
using Awar.Village;
using UnityEngine;

namespace Awar.Construction
{
    public class ConstructionController : MonoBehaviour
    {
        [SerializeField] private bool _buildMode = false;

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
        }

        private void Update()
        {
            if (_buildMode)
            {
                if (HasCancelledBuildMode()) { return; }

                Ray ray = MouseControl.MouseRay();

                if (Physics.Raycast(ray, out var hit))
                {
                    throw new System.NotImplementedException("Build-mode has not yet been implemented with the new grid");
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
        }
    }
}
