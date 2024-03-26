using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PlayScene.UI
{
    public class RestartPanel : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;
        
        [SerializeField]
        private Button _restartButton;

        [SerializeField]
        private float _fadeDuration;

        private Action _hideAction;

        private void Awake()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnRestartButtonClick()
        {
            Hide();
        }

        public void Show(Action hideAction)
        {
            _hideAction = hideAction;
            gameObject.SetActive(true);
            _canvasGroup.alpha = 0f;
            _canvasGroup.DOFade(1f, _fadeDuration);
        }

        void Hide()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.DOFade(0f, _fadeDuration).OnComplete(() =>
            {
                gameObject.SetActive(false);
                _hideAction?.Invoke();
            });
        }
    }
}