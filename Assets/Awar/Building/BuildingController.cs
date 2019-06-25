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
                    GridCell cell = _cellSelector.Hover(hit.point);
                    _placingObject.transform.position = cell.transform.position;
                }

                //TODO Place building when mouse is pressed
            } 
        }
    }
}
