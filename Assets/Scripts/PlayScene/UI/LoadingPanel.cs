using System;
using DG.Tweening;
using UnityEngine;

namespace PlayScene.UI
{
    public class LoadingPanel : MonoBehaviour
    {
        [SerializeField]
        private float _fadeDuration;
        
        [SerializeField]
        private float _loadDuration;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private RectTransform _loadingBarRectTransform;

        private Vector3 _loadingBarScale;

        private void Awake()
        {
            _loadingBarScale = _loadingBarRectTransform.localScale;
        }

        public void Show(Action hideAction)
        {
            gameObject.SetActive(true);
            _loadingBarRectTransform.localScale = Vector3.zero;
            
            Fade(0f, 1f, () =>
            {
                AnimateLoadingProgress(() =>
                {
                    Fade(1f, 0f, () =>
                    {
                        gameObject.SetActive(false);
                        hideAction.Invoke();
                    });
                });
            });
        }

        void Fade(float startAlpha, float endAlpha, TweenCallback callback)
        {
            _canvasGroup.alpha = startAlpha;
            
            DOTween.Sequence()
                .Append(_canvasGroup.DOFade(endAlpha, _fadeDuration))
                .AppendCallback(callback);
        }

        void AnimateLoadingProgress(TweenCallback callback)
        {
            _loadingBarRectTransform.localScale = new Vector3(0f, _loadingBarScale.y, _loadingBarScale.z);
            DOTween.Sequence()
                .Append(_loadingBarRectTransform.DOScaleX(_loadingBarScale.x, _loadDuration))
                .AppendCallback(callback);
        }
    }
}