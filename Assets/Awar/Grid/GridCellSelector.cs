using UnityEngine;
using UnityEngine.Scripting;

namespace Awar.Grid
{
    public class GridCellSelector
    {
        private GridController _gridController;
        private GridCell _hovered;
        private GridCell[] _hoveredRadius;

        private int _hoveredRadiusIndex;

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
            hoveredCell.EnableHover(.6f);
            return hoveredCell;
        }

        public GridCell HoverRadius(Vector3 worldPos, int radius)
        {
            RemoveRadiusHighlight();
            _hoveredRadius = new GridCell[(radius * radius) + ((radius + 1) * (radius + 1))];
            _hoveredRadiusIndex = 0;

            Vector2 gridPos = _gridController.WorldToGridPos(worldPos);

            GridCell hoveredCell = _gridController.GetCell((int)gridPos.x, (int)gridPos.y);
            hoveredCell.EnableHover(.6f);
            _hoveredRadius[_hoveredRadiusIndex] = hoveredCell;
            _hoveredRadiusIndex++;

            radius--;

            HighlightNeighbours(gridPos, Vector2.up, radius, false);
            HighlightNeighbours(gridPos, Vector2.down, radius, false);
            HighlightNeighbours(gridPos, Vector2.left, radius, false);
            HighlightNeighbours(gridPos, Vector2.right, radius, false);

            return hoveredCell;
        }

        private void HighlightNeighbours(Vector2 startPos, Vector2 direction, int steps, bool hasTurned)
        {
            Vector2 newPos = startPos + direction;
            GridCell cell = _gridController.GetCell((int)newPos.x, (int)newPos.y);

            cell.EnableHover(.25f);
            _hoveredRadius[_hoveredRadiusIndex] = cell;
            _hoveredRadiusIndex++;

            if (steps > 0)
            {
                HighlightNeighbours(newPos, direction, steps - 1, hasTurned);
                if (!hasTurned)
                {
                    HighlightNeighbours(newPos, Vector2.Perpendicular(direction), steps - 1, true);
                }
            }
        }

        private void RemoveRadiusHighlight()
        {
            if (_hoveredRadius == null)
            {
                return;
            }

            for (int i = 0; i < _hoveredRadius.Length; i++)
            {
                _hoveredRadius[i]?.DisableHover();
            }
        }
    }
}
