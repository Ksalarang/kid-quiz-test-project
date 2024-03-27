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
        private float _firstShakeDistance;

        [SerializeField]
        private int _shakeCount;

        [Header("Bounce animation")]
        [SerializeField]
        private float _bounceDuration;
        
        [Header("Components")]
        [SerializeField]
        private SpriteRenderer _cardSpriteRenderer;

        [SerializeField]
        private SpriteRenderer _backgroundRenderer;

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