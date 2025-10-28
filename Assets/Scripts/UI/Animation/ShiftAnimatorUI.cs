using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
public class ShiftAnimatorUI : MonoBehaviour, IAnimatorUI
{
    private const float EnableValue = 1f;

    [SerializeField] private ShiftUI.Direction _direction;
    [SerializeField, Range(0.1f, 2f)] private float _duration = 0.3f;
    [SerializeField, Range(0.1f, 1.5f)] private float _offsetShift = 0.5f;

    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;
    private Sequence _animation;
    private TweenController _controller;
    private Vector2 _defaultPosition;
    private ShiftUI _shift;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();

        _defaultPosition = _rectTransform.anchoredPosition;
        _controller = new TweenController();
        _shift = new ShiftUI();
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

        Vector2 startShift = _shift.GetStartPosition(_direction, _defaultPosition, _offsetShift);
        _animation = DOTween.Sequence();
        _animation
            .Append(_canvasGroup.DOFade(EnableValue, _duration).From(0))
            .Join(_rectTransform.DOAnchorPos(_defaultPosition, _duration).From(startShift))
            .SetUpdate(true);

        _controller.PlayAnimation(_animation);
        _canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        _controller.KillCurrentAnimation();

        Vector2 startShift = _shift.GetStartPosition(_direction, _defaultPosition, _offsetShift);
        _animation = DOTween.Sequence();
        _animation
            .Append(_canvasGroup.DOFade(0, _duration).From(EnableValue))
            .Join(_rectTransform.DOAnchorPos(startShift, _duration).From(_defaultPosition))
            .SetUpdate(true);

        _controller.PlayAnimation(_animation);
        _canvasGroup.blocksRaycasts = false;
    }
}
