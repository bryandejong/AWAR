using System;
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
        public void RemoveSelection()
        {
            _hovered?.DisableHover();
            RemoveRadiusHighlight();
            _hoveredRadius = null;
            _hovered = null;
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

        public GridCell HoverRadius(Vector3 worldPos, int radius, int width, int height)
        {
            RemoveRadiusHighlight();
            radius += (int)(width + height / 2f);
            _hoveredRadius = new GridCell[(radius * radius) + ((radius + 1) * (radius + 1))];
            _hoveredRadiusIndex = 0;

            Vector2 gridPos = _gridController.WorldToGridPos(worldPos);

            GridCell hoveredCell = _gridController.GetCell((int)gridPos.x, (int)gridPos.y);
            hoveredCell.EnableHover(.8f);
            _hoveredRadius[_hoveredRadiusIndex] = hoveredCell;
            _hoveredRadiusIndex++;

            radius--;

            HighlightNeighbours(gridPos, Vector2.up, radius, false, .4f);
            HighlightNeighbours(gridPos, Vector2.down, radius, false, .4f);
            HighlightNeighbours(gridPos, Vector2.left, radius, false, .4f);
            HighlightNeighbours(gridPos, Vector2.right, radius, false, .4f);

            return hoveredCell;
        }

        private void HighlightNeighbours(Vector2 startPos, Vector2 direction, int steps, bool hasTurned, float alpha)
        {
            Vector2 newPos = startPos + direction;
            GridCell cell = _gridController.GetCell((int)newPos.x, (int)newPos.y);

            if (cell == null)
            {
                return;
            }

            cell.EnableHover(alpha);
            _hoveredRadius[_hoveredRadiusIndex] = cell;
            _hoveredRadiusIndex++;

            if (steps > 0)
            {
                HighlightNeighbours(newPos, direction, steps - 1, hasTurned, alpha / 1.4f);
                if (!hasTurned)
                {
                    HighlightNeighbours(newPos, Vector2.Perpendicular(direction), steps - 1, true, alpha / 1.4f);
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
