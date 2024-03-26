using DG.Tweening;
using PlayScene.Gameplay;
using TMPro;
using UnityEngine;
using Zenject;

namespace PlayScene.UI
{
    public class TaskLabel : MonoBehaviour
    {
        [SerializeField]
        private float _fadeDuration;
        
        [Inject]
        private GameplayController _gameplayController;
        
        private TMP_Text label;

        private void Awake()
        {
            label = GetComponent<TMP_Text>();

            _gameplayController.OnTaskCardSelected += cardId =>
            {
                label.text = $"Find {cardId}";
            };
        }

        public void Show()
        {
            label.alpha = 0f;
            label.DOFade(1f, _fadeDuration);
        }
    }
}