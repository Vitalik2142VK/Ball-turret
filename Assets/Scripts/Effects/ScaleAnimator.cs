using CannonTurret.UI.Animations;
using DG.Tweening;
using UnityEngine;

namespace CannonTurret.Effects
{
    public class ScaleAnimator : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 2f)] private float _duration = 0.3f;
        [SerializeField, Range(0.05f, 1.5f)] private float _startSizeValue = 0.5f;

        private Transform _transform;
        private Tween _animation;
        private TweenController _controller;
        private Vector3 _defaultSize;
        private Vector3 _startSize;

        private void Awake()
        {
            Initicalize();
        }

        private void OnDestroy()
        {
            _controller.KillCurrentAnimation();
        }

        public void Initicalize()
        {
            if (_transform != null)
                return;

            _transform = transform;
            _controller = new TweenController();

            UpdateScale();
        }

        public YieldInstruction GetYieldAnimation() => _controller.GetYieldAnimation();

        public void Show()
        {
            _controller.KillCurrentAnimation();

            _animation = _transform.DOScale(_defaultSize, _duration).From(_startSize);
            _controller.PlayAnimation(_animation);
        }

        public void Hide()
        {
            _controller.KillCurrentAnimation();

            _animation = _transform.DOScale(_startSize, _duration).From(_defaultSize);
            _controller.PlayAnimation(_animation);
        }

        public void UpdateScale()
        {
            _defaultSize = _transform.localScale;
            _startSize = _defaultSize * _startSizeValue;
        }
    }
}