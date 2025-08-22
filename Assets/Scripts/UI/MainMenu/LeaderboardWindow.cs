using System;
using UnityEngine;

[RequireComponent(typeof(HiderUI))]
public class LeaderboardWindow : MonoBehaviour
{
    private IMenu _previousMenu;
    private HiderUI _hiderUI;

    private void Awake()
    {
        gameObject.SetActive(false);
        _hiderUI = GetComponent<HiderUI>();
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
        _previousMenu.Enable();
        _hiderUI.Enable();
    }

    public void Open(IMenu previousMenu)
    {
        _previousMenu = previousMenu ?? throw new ArgumentNullException(nameof(previousMenu));
        gameObject.SetActive(true);
        _hiderUI.Disable();
    }
}
