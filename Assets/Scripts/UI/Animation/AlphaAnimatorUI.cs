using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class AlphaAnimatorUI : MonoBehaviour, IAnimatorUI
{
    private const float EnableValue = 1f;

    [SerializeField, Range(0.1f, 3f)] private float _showDuration = 0.3f;
    [SerializeField, Range(0.1f, 3f)] private float _hideDuration = 0.3f;

    private CanvasGroup _canvasGroup;
    private Tween _animation;
    private TweenController _controller;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _controller = new TweenController();
    }

    private void OnDestroy()
    {
        _controller.KillCurrentAnimation();
    }

    public YieldInstruction GetYieldAnimation() => _controller.GetYieldAnimation();

    public void Show()
    {
        _controller.KillCurrentAnimation();
        _animation = _canvasGroup
            .DOFade(EnableValue, _showDuration).From(0)
            .SetUpdate(true);

        _controller.PlayAnimation(_animation);
    }

    public void Hide()
    {
        _controller.KillCurrentAnimation();
        _animation = _canvasGroup
            .DOFade(0, _hideDuration).From(EnableValue)
            .SetUpdate(true);

        _controller.PlayAnimation(_animation);
    }
}
