using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
public class ScaleAnimatorUI : MonoBehaviour, IAnimatorUI
{
    private const float EnableValue = 1f;

    [SerializeField, Range(0.1f, 2f)] private float _duration = 0.3f;
    [SerializeField, Range(0.1f, 1.5f)] private float _startSizeValue = 0.5f;

    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;
    private Sequence _animation;
    private TweenController _controller;
    private Vector2 _defaultSize;
    private Vector2 _startSize;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();

        _defaultSize = _rectTransform.localScale;
        _controller = new TweenController();
        _startSize = new Vector2(_defaultSize.x * _startSizeValue, _defaultSize.y * _startSizeValue);
    }

    private void Start()
    {
        _canvasGroup.alpha = EnableValue;
        _canvasGroup.blocksRaycasts = true;
    }

    private void OnDestroy()
    {
        _controller.KillCurrentAnimation();
    }

    public YieldInstruction GetYieldAnimation() => _controller.GetYieldAnimation();

    public void Show()
    {
        _controller.KillCurrentAnimation();

        _animation = DOTween.Sequence();
        _animation
            .Append(_canvasGroup.DOFade(EnableValue, _duration).From(0))
            .Join(_rectTransform.DOScale(_defaultSize, _duration).From(_startSize))
            .SetUpdate(true);

        _controller.PlayAnimation(_animation);
        _canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        _controller.KillCurrentAnimation();

        _animation = DOTween.Sequence();
        _animation
            .Append(_canvasGroup.DOFade(0, _duration).From(EnableValue))
            .Join(_rectTransform.DOScale(_startSize, _duration).From(_defaultSize))
            .SetUpdate(true);

        _controller.PlayAnimation(_animation);
        _canvasGroup.blocksRaycasts = false;
    }
}
