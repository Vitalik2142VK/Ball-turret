using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ScaleButtonAnimator : MonoBehaviour, IButtonAnimator
{
    [SerializeField, Range(0.05f, 0.95f)] private float _clickSizeValue = 0.1f;
    [SerializeField, Range(0.05f, 0.3f)] private float _duration = 0.1f;

    private Tween _animation;
    private RectTransform _rectTransform;
    private Vector2 _defaultSize;
    private Vector2 _clickSize;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        _defaultSize = _rectTransform.localScale;
        _clickSize = new Vector2(_defaultSize.x - _clickSizeValue, _defaultSize.y - _clickSizeValue);
    }

    private void OnDestroy()
    {
        KillCurrentAnimation();
    }

    public void Press()
    {
        KillCurrentAnimation();

        _animation = _rectTransform.DOScale(_clickSize, _duration).From(_defaultSize)
            .SetUpdate(true)
            .Play();
    }

    public void PressOut()
    {
        Vector2 currentScale = _rectTransform.localScale;

        if (currentScale == _defaultSize)
            return;

        KillCurrentAnimation();

        _animation = _rectTransform.DOScale(_defaultSize, _duration).From(_clickSize)
            .SetUpdate(true)
            .Play();
    }

    private void KillCurrentAnimation()
    {
        if (_animation != null && _animation.active)
            _animation.Kill();
    }
}