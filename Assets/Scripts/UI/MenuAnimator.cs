using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
public class MenuAnimator : MonoBehaviour, IUIAnimator
{
    private const float EnableValue = 1f;
    private const float OffsetShift = 0.5f;

    [SerializeField, Range(0.1f, 2f)] private float _duration = 0.5f;

    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;
    private Sequence _animation;
    private Vector2 _mainPosition;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();
        _mainPosition = _rectTransform.anchoredPosition;
    }

    private void Start()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
    }

    private void OnDestroy()
    {
        KillCurrentAnimation();
    }

    public YieldInstruction PlayOpen()
    {
        KillCurrentAnimation();

        float shift = -Screen.height * OffsetShift;
        Vector2 startShift = new Vector2(_mainPosition.x, shift);
        _animation = DOTween.Sequence();
        _animation
            .Append(_canvasGroup.DOFade(EnableValue, _duration).From(0))
            .Join(_rectTransform.DOAnchorPos(_mainPosition, _duration).From(startShift))
            .Play();

        _canvasGroup.blocksRaycasts = true;

        return _animation.WaitForCompletion();
    }

    public YieldInstruction PlayClose()
    {
        KillCurrentAnimation();

        float shift = -Screen.height * OffsetShift;
        Vector2 startShift = new Vector2(_mainPosition.x, shift);
        _animation = DOTween.Sequence();
        _animation
            .Append(_canvasGroup.DOFade(0, _duration).From(EnableValue))
            .Join(_rectTransform.DOAnchorPos(startShift, _duration).From(_mainPosition))
            .Play();

        _canvasGroup.blocksRaycasts = false;

        return _animation.WaitForCompletion();
    }

    private void KillCurrentAnimation()
    {
        if (_animation != null && _animation.active)
            _animation.Kill();
    }
}
