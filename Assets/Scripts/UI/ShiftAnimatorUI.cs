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
    private Vector2 _defaultPosition;
    private ShiftUI _shift;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();

        _defaultPosition = _rectTransform.anchoredPosition;
        _shift = new ShiftUI();
    }

    private void Start()
    {
        _canvasGroup.alpha = EnableValue;
        _canvasGroup.blocksRaycasts = true;
    }

    private void OnDestroy()
    {
        KillCurrentAnimation();
    }

    public void Show()
    {
        KillCurrentAnimation();

        Vector2 startShift = _shift.GetStartPosition(_direction, _defaultPosition, _offsetShift);
        _animation = DOTween.Sequence();
        _animation
            .Append(_canvasGroup.DOFade(EnableValue, _duration).From(0))
            .Join(_rectTransform.DOAnchorPos(_defaultPosition, _duration).From(startShift))
            .SetUpdate(true)
            .Play();

        _canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        KillCurrentAnimation();

        Vector2 startShift = _shift.GetStartPosition(_direction, _defaultPosition, _offsetShift);
        _animation = DOTween.Sequence();
        _animation
            .Append(_canvasGroup.DOFade(0, _duration).From(EnableValue))
            .Join(_rectTransform.DOAnchorPos(startShift, _duration).From(_defaultPosition))
            .SetUpdate(true)
            .Play();

        _canvasGroup.blocksRaycasts = false;
    }

    public YieldInstruction GetYieldAnimation()
    {
        if (_animation == null || _animation.active == false)
            throw new System.InvalidOperationException("Animation was not launched");

        return _animation.WaitForCompletion();
    }

    private void KillCurrentAnimation()
    {
        if (_animation != null && _animation.active)
            _animation.Kill();
    }
}
