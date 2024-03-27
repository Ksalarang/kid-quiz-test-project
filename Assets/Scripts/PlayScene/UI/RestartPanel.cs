using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PlayScene.UI
{
    public class RestartPanel : MonoBehaviour
    {
        [SerializeField]
        private float _fadeDuration;
        
        [SerializeField]
        private CanvasGroup _canvasGroup;
        
        [SerializeField]
        private Button _restartButton;

        [Inject]
        private LoadingPanel _loadingPanel;

        private Tween _fadeTween;

        private Action _hideAction;

        private void Awake()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnRestartButtonClick()
        {
            Fade(1f, 0f, () =>
            {
                gameObject.SetActive(false);
            });
            _loadingPanel.Show(_hideAction);
        }

        public void Show(Action hideAction)
        {
            _hideAction = hideAction;
            gameObject.SetActive(true);
            Fade(0f, 1f);
        }

        private void Fade(float startAlpha, float endAlpha, TweenCallback callback = null)
        {
            _fadeTween?.Kill();
            
            _canvasGroup.alpha = startAlpha;
            _fadeTween = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(endAlpha, _fadeDuration))
                .AppendCallback(callback);
        }
    }
}