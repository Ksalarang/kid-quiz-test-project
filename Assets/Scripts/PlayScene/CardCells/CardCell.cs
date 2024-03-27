using System;
using DG.Tweening;
using PlayScene.Data.Cards;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayScene.CardCells
{
    public class CardCell : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private float _shakeDuration;

        [SerializeField]
        private float _firstShakeDistance;

        [SerializeField]
        private int _shakeCount;
        
        [SerializeField]
        private SpriteRenderer _cardSpriteRenderer;

        private Transform _cardTransform;

        private Action<CardCell> _cellClick;

        private Sequence _shakeSequence;

        private CardData _cardData;

        public CardData CardData => _cardData;

        private void Awake()
        {
            _cardTransform = _cardSpriteRenderer.transform;
        }

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
            _shakeSequence?.Kill();

            _shakeSequence = DOTween.Sequence();
            var initialX = _cardTransform.localPosition.x;
            var oneShakeDuration = _shakeDuration / (_shakeCount + 1);
            
            for (var i = 0; i < _shakeCount; i++)
            {
                var x = initialX + _firstShakeDistance + _firstShakeDistance * i;
                if (i % 2 != 0) x = -x;
                _shakeSequence.Append(_cardTransform.DOLocalMoveX(x, oneShakeDuration));
            }
            _shakeSequence.Append(_cardTransform.DOLocalMoveX(initialX, oneShakeDuration));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _cellClick?.Invoke(this);
        }
    }
}