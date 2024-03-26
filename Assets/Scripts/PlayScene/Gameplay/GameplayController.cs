using System.Collections.Generic;
using System.Linq;
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

        private LevelData _currentLevel;
        
        private int _currentLevelIndex;

        private CardData _correctCard;

        private void Awake()
        {
            _cells = new List<CardCell>();
        }

        private void Start()
        {
            ResetLevelIndex();
            StartNextLevel();
        }

        private void ResetLevelIndex()
        {
            _currentLevelIndex = -1;
        }

        private void StartNextLevel()
        {
            _currentLevel = _levelBundleData.LevelDataList[++_currentLevelIndex];
            
            CreateCellGrid();
            InitializeCells();
        }

        private void CreateCellGrid()
        {
            _cardCellFactory.DestroyCells(_cells);
            _cells = _cardCellFactory.GetCells(_currentLevel.CellAmount);
            _cardCellGrid.PositionCells(_cells, GetGridSize(_currentLevel));
        }

        private Vector2Int GetGridSize(LevelData levelData)
        {
            var x = _levelBundleData.GridWidth;
            var y = Mathf.CeilToInt((float) levelData.CellAmount / _levelBundleData.GridWidth);
            return new Vector2Int(x, y);
        }


        private void InitializeCells()
        {
            var cards = GetRandomCardList();
            
            _correctCard = GetRandomCard(cards);

            for (var i = cards.Count - 1; i >= 0; i--)
            {
                var card = cards[i];
                
                var cell = _cells[i];
                cell.SetCard(card);
                cell.SetClickAction(OnCardClick);

                cards.Remove(card);
            }
        }

        private List<CardData> GetRandomCardList()
        {
            var totalCards = _currentLevel.CardBundleData.CardDataList.ToList();
            var cards = new List<CardData>();

            for (var i = _currentLevel.CellAmount - 1; i >= 0; i--)
            {
                var card = GetRandomCard(totalCards);
                cards.Add(card);
                totalCards.Remove(card);
            }
            return cards;
        }

        private CardData GetRandomCard(List<CardData> cards)
        {
            return cards.Count == 0 ? null : cards[Random.Range(0, cards.Count)];
        }
        
        private void OnCardClick(CardData cardData)
        {
            if (cardData == _correctCard)
            {
                OnCorrectCardSelected();
            }
        }

        private void OnCorrectCardSelected()
        {
            if (IsLastLevel())
            {
                ResetLevelIndex();
            }
            StartNextLevel();
        }

        private bool IsLastLevel() => _currentLevel == _levelBundleData.LevelDataList.Last();
    }
}