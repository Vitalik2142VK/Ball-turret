using System;
using UnityEngine;

[RequireComponent(typeof(OpenWindowButton))]
public class AdvancedOpenWindowButton : MonoBehaviour, IAdvancedOpenWindowButton
{
    private OpenWindowButton _openWindowButton;

    public event Action ActivityСhangedCliced;

    private void Awake()
    {
        _openWindowButton = GetComponent<OpenWindowButton>();
    }

    private void OnEnable()
    {
        ActivityСhangedCliced?.Invoke();
    }

    private void OnDisable()
    {
        ActivityСhangedCliced?.Invoke();
    }

    public bool IsActive => _openWindowButton.IsActive;

    public void Show() => _openWindowButton.Show();

    public void Hide() => _openWindowButton.Hide();
}
