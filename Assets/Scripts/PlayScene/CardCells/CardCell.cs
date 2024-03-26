using System;
using PlayScene.Data.Cards;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayScene.CardCells
{
    public class CardCell : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private SpriteRenderer _cardSpriteRenderer;

        private Transform _cardTransform;

        private CardData _cardData;

        private Action<CardData> _cardClick;

        private void Awake()
        {
            _cardTransform = _cardSpriteRenderer.transform;
        }

        public void SetCard(CardData cardData)
        {
            _cardData = cardData;
            _cardSpriteRenderer.sprite = cardData.Sprite;
        }

        public void SetClickAction(Action<CardData> action)
        {
            _cardClick = action;
        }

        public void SetCardRotationZ(float angleDegrees)
        {
            var eulerAngles = _cardTransform.eulerAngles;
            eulerAngles.z = angleDegrees;
            _cardTransform.eulerAngles = eulerAngles;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _cardClick?.Invoke(_cardData);
        }
    }
}