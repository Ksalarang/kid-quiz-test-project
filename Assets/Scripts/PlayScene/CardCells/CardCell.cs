using System;
using DG.Tweening;
using PlayScene.Data.Cards;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayScene.CardCells
{
    public class CardCell : MonoBehaviour, IPointerClickHandler
    {
        [Header("Shake animation")]
        [SerializeField]
        private float _shakeDuration;

        [SerializeField]
        private float _shakeDistance;

        [Header("Bounce animation")]
        [SerializeField]
        private float _bounceDuration;
        
        [Header("Components")]
        [SerializeField]
        private SpriteRenderer _cardSpriteRenderer;

        [SerializeField]
        private Transform _cardTransform;
        
        [SerializeField]
        private SpriteRenderer _backgroundRenderer;

        private Action<CardCell> _cellClick;

        private Tween _shakeTween;

        private CardData _cardData;

        public CardData CardData => _cardData;

        public void SetCard(CardData cardData)
        {
            _cardData = cardData;
            _cardSpriteRenderer.sprite = cardData.Sprite;
        }

        public void SetClickAction(Action<CardCell> action)
        {
            _cellClick = action;
        }

        public void SetCardRotationZ(float angleDegrees)
        {
            var eulerAngles = _cardTransform.eulerAngles;
            eulerAngles.z = angleDegrees;
            _cardTransform.eulerAngles = eulerAngles;
        }

        public void AnimateIncorrectAnswer()
        {
            _shakeTween?.Kill(true);
            
            var initialPosition = _cardTransform.localPosition;
            _cardTransform.localPosition = new Vector3(-_shakeDistance, initialPosition.y, initialPosition.z);
            
            _shakeTween = _cardTransform
                .DOLocalMoveX(initialPosition.x, _shakeDuration)
                .SetEase(Ease.OutElastic);
        }

        public float AnimateCorrectAnswer(Action endAction)
        {
            var initialScale = _cardTransform.localScale;
            _cardTransform.localScale = initialScale * 0.75f;

            var sequence = DOTween.Sequence();
            sequence.Append(_cardTransform.DOScale(initialScale, _bounceDuration).SetEase(Ease.OutElastic));
            sequence.AppendCallback(endAction.Invoke);
            
            return _bounceDuration;
        }

        public void SetBackgroundColor(Color color)
        {
            _backgroundRenderer.color = color;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _cellClick?.Invoke(this);
        }
    }
}