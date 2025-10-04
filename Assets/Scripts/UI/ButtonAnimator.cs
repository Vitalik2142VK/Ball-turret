using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Button))]
public class ButtonAnimator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField, Range(0.05f, 0.95f)] private float _clickSizeValue = 0.15f;
    [SerializeField, Range(0.05f, 0.3f)] private float _duration = 0.1f;

    private Tween _animation;
    private RectTransform _rectTransform;
    private Button _button;
    private Vector2 _defaultSize;
    private Vector2 _clickSize;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _button = GetComponent<Button>();
        _defaultSize = _rectTransform.localScale;
        _clickSize = new Vector2(_defaultSize.x - _clickSizeValue, _defaultSize.y - _clickSizeValue);
    }

    private void OnDestroy()
    {
        KillCurrentAnimation();
    }

    public void OnPointerDown(PointerEventData _)
    {
        if (_button.interactable == false)
            return;

        KillCurrentAnimation();

        _animation = _rectTransform.DOScale(_clickSize, _duration).Play();
    }

    public void OnPointerUp(PointerEventData _)
    {
        if (_button.interactable == false)
            return;

        KillCurrentAnimation();

        _animation = _rectTransform.DOScale(_defaultSize, _duration).Play();
    }

    private void KillCurrentAnimation()
    {
        if (_animation != null && _animation.active)
            _animation.Kill();
    }
}