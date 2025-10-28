using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IAnimatorUI), typeof(Button))]
public class OpenWindowButton : MonoBehaviour, IOpenWindowButton
{
    [SerializeField, SerializeIterface(typeof(IWindow))] private GameObject _windowGameObject;

    private Button _button;
    private IAnimatorUI _animator;
    private IWindow _window;

    private void OnValidate()
    {
        if (_windowGameObject == null)
            throw new NullReferenceException(nameof(_windowGameObject));
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _animator = GetComponent<IAnimatorUI>();
        _window = _windowGameObject.GetComponent<IWindow>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public void SetPauseMenu(IWindow window)
    {
        _window = window ?? throw new ArgumentNullException(nameof(window));
    }

    public void Show()
    {
        if (gameObject.activeSelf)
            return;

        gameObject.SetActive(true);

        _animator.Show();
    }

    private void OnClick()
    {
        _window.Enable();
        _animator.Hide();

        StartCoroutine(WaitClosure());
    }

    private IEnumerator WaitClosure()
    {
        yield return _animator.GetYieldAnimation();

        gameObject.SetActive(false);
    }
}
