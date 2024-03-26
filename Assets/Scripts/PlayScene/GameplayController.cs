using System.Collections.Generic;
using PlayScene.CardCells;
using UnityEngine;
using Zenject;

namespace PlayScene
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField]
        private int _cellCount;

        [SerializeField]
        private Vector2Int _gridSize;

        [Inject]
        private CardCellFactory _cardCellFactory;

        [Inject]
        private CardCellGrid _cardCellGrid;

        private List<CardCell> _cells;

        private void Awake()
        {
            _cells = new List<CardCell>();
        }

        private void Start()
        {
            RespawnCellGrid();
        }

        private void RespawnCellGrid()
        {
            _cardCellFactory.DestroyCells(_cells);
            _cells = _cardCellFactory.GetList(_cellCount);
            _cardCellGrid.PositionCardCells(_cells, _gridSize);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.R))
            {
                RespawnCellGrid();
            }
        }
    }
}