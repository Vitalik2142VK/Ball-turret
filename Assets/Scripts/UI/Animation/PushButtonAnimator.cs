using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(ScaleButtonAnimator))]
public class PushButtonAnimator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private IButtonAnimator _animator;
    private Button _button;

    private void Awake()
    {
        _animator = GetComponent<IButtonAnimator>();
        _button = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData _)
    {
        if (_button.interactable == false)
            return;

        _animator.Press();
    }

    public void OnPointerUp(PointerEventData _)
    {
        if (_button.interactable == false)
            return;

        _animator.PressOut();
    }
}
