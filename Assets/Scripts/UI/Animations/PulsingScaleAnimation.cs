using DG.Tweening;
using UnityEngine;

namespace CannonTurret.UI.Animations
{
    public class PulsingScaleAnimation : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 2f)] private float _duration = 0.3f;
        [SerializeField, Range(.1f, 2f)] private float _scaleCoeficient = 1.5f;

        private Transform _transform;
        private TweenController _controller;
        private Sequence _animation;
        private Vector3 _defaultScale;
        private Vector3 _waveScale;

        private void Awake()
        {
            _controller = new TweenController();

            _transform = transform;
            _defaultScale = _transform.localScale;
            _waveScale = _defaultScale * _scaleCoeficient;
        }

        private void OnDisable()
        {
            _controller.KillCurrentAnimation();
        }

        public void Play()
        {
            _animation = DOTween.Sequence();
            _animation
                .Append(_transform.DOScale(_waveScale, _duration).From(_defaultScale))
                .Append(_transform.DOScale(_defaultScale, _duration).From(_waveScale))
                .SetUpdate(true);

            _controller.PlayAnimation(_animation);
        }
    }
}