using DG.Tweening;
using UnityEngine;

namespace PlayScene.UI
{
    public class ClickBlocker : MonoBehaviour
    {
        public void Show(float duration)
        {
            gameObject.SetActive(true);
            
            DOTween.Sequence()
                .AppendInterval(duration)
                .AppendCallback(() =>
                {
                    gameObject.SetActive(false);
                });
        }
    }
}