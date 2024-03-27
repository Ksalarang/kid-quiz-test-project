using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayScene.CardCells
{
    public class CardCellGrid : MonoBehaviour
    {
        [SerializeField]
        private Vector2 _gridPosition;

        public void PositionCells(IList<CardCell> cells, Vector2Int gridSize)
        {
            var cellScale = cells.First().transform.localScale.x;
            var bottomLeftPosition = new Vector2
            {
                x = _gridPosition.x - cellScale * (gridSize.x - 1) / 2,
                y = _gridPosition.y - cellScale * (gridSize.y - 1) / 2
            };

            for (var i = 0; i < cells.Count; i++)
            {
                var x = bottomLeftPosition.x + i % gridSize.x * cellScale;
                var y = bottomLeftPosition.y + i / gridSize.x * cellScale;

                cells[i].transform.localPosition = new Vector3(x, y);
            }
        }
    }
}