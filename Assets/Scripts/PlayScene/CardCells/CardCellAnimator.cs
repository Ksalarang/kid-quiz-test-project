using DG.Tweening;
using UnityEngine;

namespace PlayScene.CardCells
{
    public class CardCellAnimator : MonoBehaviour
    {
        [SerializeField]
        private float _showDuration;

        [SerializeField]
        private float _amplitude;

        [SerializeField]
        private float _period;
        
        [SerializeField]
        private Transform cellsTransform;

        public void Show()
        {
            SetVisible(true);
            
            var defaultScale = cellsTransform.localScale;
            cellsTransform.localScale = Vector3.zero;
            cellsTransform.DOScale(defaultScale, _showDuration).SetEase(Ease.OutElastic, _amplitude, _period);
        }

        public void SetVisible(bool visible)
        {
            cellsTransform.gameObject.SetActive(visible);
        }
    }
}