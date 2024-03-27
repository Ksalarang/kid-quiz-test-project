using System;
using DG.Tweening;
using UnityEngine;

namespace PlayScene.Particles
{
    public class StarParticleController : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _particles;

        public void RestartAt(Vector3 position, float playDuration)
        {
            _particles.transform.position = position;
            _particles.Play();
            
            DOTween.Sequence()
                .AppendInterval(playDuration)
                .AppendCallback(() =>
                {
                    _particles.Clear();
                    _particles.Stop();
                });
        }
    }
}