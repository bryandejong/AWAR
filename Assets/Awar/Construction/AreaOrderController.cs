using Awar.Core;
using Awar.Grid;
using Awar.Utils;
using UnityEngine;

namespace Awar.Construction
{
    public class AreaOrderController : AwarBehavior
    {
        [SerializeField] private OrderMode _mode = OrderMode.None;
        [SerializeField] private AreaHover _areaHover = default;

        private Vector3 _dragStartPos;
        private Vector3 _dragEndPos;

        public override void Initialize()
        {
        }

        public override void Tick()
        {
            if (_mode == OrderMode.None) return;
            if (HasCancelledOrderMode()) return;

            Ray ray = MouseControl.MouseRay();
            if (Physics.Raycast(ray, out RaycastHit hit, 200))
            {
                Vector3 hitPos = GridController.SnapToGrid(hit.point);

                if (Input.GetMouseButtonDown(0))
                {
                    _dragStartPos = hitPos;
                    _areaHover.SetPosition(_dragStartPos);
                }

                if (Input.GetMouseButton(0))
                {
                    _dragEndPos = hitPos;
                    Vector3 correctedStartPos = _dragStartPos;
                    Vector3 difference = _dragStartPos - _dragEndPos;
                    if ((int)difference.x < 0) difference.x -= 1;
                    if ((int)difference.x > 0) difference.x += 1;
                    if ((int)difference.z < 0) difference.z -= 1;
                    if ((int)difference.z > 0) difference.z += 1;
                    if ((int)difference.x == 0) difference.x = -1;
                    if ((int)difference.z == 0) difference.z = 1;

                    // Horizontal fix
                    if ((int)difference.x >= 1)
                    {
                        correctedStartPos += new Vector3(1, 0, 0);

                    }

                    // Vertical fix
                    if ((int)difference.z <= -1)
                    {
                        correctedStartPos -= new Vector3(0, 0, 1);
                    }

                    Vector2 dimensions = new Vector2(-difference.x, -difference.z);
                    _areaHover.SetPosition(correctedStartPos);
                    _areaHover.SetDimensions(dimensions);

                    Vector3 gridCorrectedPos = _dragStartPos - new Vector3(0, 0, 1);
                    // Mark all trees
                }
                else
                {
                    _areaHover.SetPosition(hitPos);
                }
            }
        }

        public void SetMode(OrderMode mode)
        {
            _mode = mode;
            if (mode != OrderMode.None)
            {
                _areaHover.gameObject.SetActive(true);
            }
        }

        public void SetMode(int mode)
        {
            SetMode((OrderMode)mode);
        }

        private bool HasCancelledOrderMode()
        {
            if (!Input.GetKey(KeyCode.Escape) && !Input.GetKey(KeyCode.B)) return false;

            _areaHover.gameObject.SetActive(false);
            SetMode(OrderMode.None);
            return true;

        }
    }

    public enum OrderMode
    {
        None = 0, Woodcutting = 1
    }
}
