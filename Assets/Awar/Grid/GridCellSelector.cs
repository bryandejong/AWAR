using UnityEngine;

namespace Awar.Grid
{
    public class GridCellSelector
    {
        private GridController _gridController;
        private GridCell _hovered;
        public GridCellSelector(GridController gridController)
        {
            _gridController = gridController;
        }

        public GridCell Hover(Vector3 worldPos)
        {
            GridCell hoveredCell = _gridController.GetCell(worldPos);
            if (hoveredCell != _hovered)
            {
                if (_hovered != null)
                {
                    _hovered.DisableHover();
                }

                _hovered = hoveredCell;
            }
            hoveredCell.EnableHover();
            return hoveredCell;
        }
    }
}
