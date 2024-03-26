using System;
using PlayScene.Data.Cards;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayScene.CardCells
{
    public class CardCell : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private CardData _cardData;

        private Action<CardData> _cardClick;

        public void SetCard(CardData cardData)
        {
            _cardData = cardData;
            _spriteRenderer.sprite = cardData.Sprite;
        }

        public void SetClickAction(Action<CardData> action)
        {
            _cardClick = action;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _cardClick?.Invoke(_cardData);
        }
    }
}