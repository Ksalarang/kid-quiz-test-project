using System.Collections.Generic;
using UnityEngine;

namespace PlayScene.CardCells
{
    public class CardCellGrid : MonoBehaviour
    {
        [SerializeField]
        private Vector2 _gridPosition;

        [SerializeField]
        private float _cellScale;

        public void PositionCells(IList<CardCell> cells, Vector2Int gridSize)
        {
            var bottomLeftPosition = new Vector2
            {
                x = _gridPosition.x - _cellScale * (gridSize.x - 1) / 2,
                y = _gridPosition.y - _cellScale * (gridSize.y - 1) / 2
            };

            for (var i = 0; i < cells.Count; i++)
            {
                var x = bottomLeftPosition.x + i % gridSize.x * _cellScale;
                var y = bottomLeftPosition.y + i / gridSize.x * _cellScale;

                cells[i].transform.localPosition = new Vector3(x, y);
            }
        }
    }
}