using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(IAnimatorUI))]
public class ActivationButton : MonoBehaviour
{
    private IAnimatorUI _animator;
    private Button _button;

    public event Action Clicked;

    private void Awake()
    {
        _animator = GetComponent<IAnimatorUI>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClicked);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _animator.Show();
    }

    public void Hide() 
    {
        _animator.Hide();

        StartCoroutine(WaitClosure());
    }

    private void OnClicked()
    {
        Clicked?.Invoke();
    }

    private IEnumerator WaitClosure()
    {
        yield return _animator.GetYieldAnimation();

        gameObject.SetActive(false);
    }
}