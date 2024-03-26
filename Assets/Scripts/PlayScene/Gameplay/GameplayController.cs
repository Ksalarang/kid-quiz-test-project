using System.Collections.Generic;
using PlayScene.CardCells;
using PlayScene.Data.Cards;
using PlayScene.Data.Levels;
using UnityEngine;
using Zenject;

namespace PlayScene.Gameplay
{
    public class GameplayController : MonoBehaviour
    {
        [Inject]
        private CardCellFactory _cardCellFactory;

        [Inject]
        private CardCellGrid _cardCellGrid;

        [Inject]
        private LevelBundleData _levelBundleData;

        private List<CardCell> _cells;

        private LevelData _currentLevelData;
        
        private int _currentLevelIndex = -1;

        private void Awake()
        {
            _cells = new List<CardCell>();
        }

        private void Start()
        {
            StartNextLevel();
        }

        private void StartNextLevel()
        {
            _currentLevelData = _levelBundleData.LevelDataList[++_currentLevelIndex];
            
            CreateGrid();
        }

        private void CreateGrid()
        {
            _cardCellFactory.DestroyCells(_cells);
            _cells = _cardCellFactory.GetCells(_currentLevelData.CellAmount);
            _cardCellGrid.PositionCells(_cells, GetGridSize(_currentLevelData));
        }

        private Vector2Int GetGridSize(LevelData levelData)
        {
            var x = _levelBundleData.GridWidth;
            var y = Mathf.CeilToInt((float) levelData.CellAmount / _levelBundleData.GridWidth);
            return new Vector2Int(x, y);
        }
    }
}