﻿using System.Collections.Generic;
using System.Linq;
using PlayScene.CardCells;
using PlayScene.Data.Cards;
using PlayScene.Data.Levels;
using PlayScene.Particles;
using PlayScene.UI;
using UnityEngine;
using Zenject;

namespace PlayScene.Gameplay
{
    public class GameplayController : MonoBehaviour
    {
        public OnTaskCardSelected OnTaskCardSelected;
        
        [Inject]
        private CardCellFactory _cardCellFactory;

        [Inject]
        private CardCellGrid _cardCellGrid;

        [Inject]
        private LevelBundleData _levelBundleData;

        [Inject]
        private RestartPanel _restartPanel;

        [Inject]
        private TaskLabel _taskLabel;

        [Inject]
        private CardCellAnimator _cardCellAnimator;

        [Inject]
        private StarParticleController _starParticles;

        [Inject]
        private ClickBlocker _clickBlocker;

        private List<CardCell> _cells;

        private LevelData _currentLevel;
        
        private int _currentLevelIndex;

        private CardData _correctCard;

        private List<CardData> _usedCards;

        private void Awake()
        {
            _cells = new List<CardCell>();
            _usedCards = new List<CardData>();
        }

        private void Start()
        {
            ResetLevelIndex();
            StartNextLevel();
            ShowUI();
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

        private void ShowUI()
        {
            _cardCellAnimator.Show();
            _taskLabel.Show();
        }

        private void HideUI()
        {
            _cardCellAnimator.SetVisible(false);
            _taskLabel.SetVisible(false);
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
            var totalCards = _currentLevel.CardBundleData.CardDataList.ToList();
            
            SelectCorrectCard(totalCards);

            var cards = GetCardsForCurrentLevel(totalCards);

            for (var i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                var cell = _cells[i];
                
                cell.SetCard(card);
                cell.SetClickAction(OnCardClick);
                FixCardSpriteRotation(cell, card);
            }
        }
        
        private void SelectCorrectCard(List<CardData> cards)
        {
            CardData correctCard;
            var count = 0;
            
            do
            {
                correctCard = GetRandomCard(cards);
                count++;
            } while (_usedCards.Contains(correctCard) && count < cards.Count);

            if (count == cards.Count)
            {
                Debug.LogError("All cards are used. Amount of levels must be <= than card bundle size");
            }
            
            _correctCard = correctCard;
            _usedCards.Add(correctCard);
            
            OnTaskCardSelected?.Invoke(correctCard.Identifier);
        }

        private List<CardData> GetCardsForCurrentLevel(List<CardData> totalCards)
        {
            totalCards.Remove(_correctCard);
            
            var cards = GetUniqueRandomCards(totalCards, _currentLevel.CellAmount - 1);
            
            var randomIndex = Random.Range(0, _currentLevel.CellAmount);
            cards.Insert(randomIndex, _correctCard);
            
            return cards;
        }

        private List<CardData> GetUniqueRandomCards(List<CardData> cards, int count)
        {
            var result = new List<CardData>();

            for (var i = count - 1; i >= 0; i--)
            {
                var card = GetRandomCard(cards);
                result.Add(card);
                cards.Remove(card);
            }
            return result;
        }

        private CardData GetRandomCard(List<CardData> cards)
        {
            return cards.Count == 0 ? null : cards[Random.Range(0, cards.Count)];
        }

        private void FixCardSpriteRotation(CardCell cell, CardData card)
        {
            if (card.Identifier == "7" || card.Identifier == "8")
            {
                cell.SetCardRotationZ(-90);
            }
        }

        private void OnCardClick(CardCell cell)
        {
            if (cell.CardData == _correctCard)
            {
                var duration = cell.AnimateCorrectAnswer(OnCorrectCardSelected);
                _starParticles.RestartAt(cell.transform.position, duration);
                _clickBlocker.Show(duration);
            }
            else
            {
                cell.AnimateIncorrectAnswer();
            }
        }

        private void OnCorrectCardSelected()
        {
            if (IsLastLevel())
            {
                ResetLevelIndex();
                _usedCards.Clear();
                
                _restartPanel.Show(
                    () =>
                    {
                        HideUI();
                    }, 
                    () => 
                    {
                        StartNextLevel();
                        ShowUI();
                    });
            }
            else
            {
                StartNextLevel();
            }
        }

        private bool IsLastLevel() => _currentLevel == _levelBundleData.LevelDataList.Last();
    }

    public delegate void OnTaskCardSelected(string cardId);
}