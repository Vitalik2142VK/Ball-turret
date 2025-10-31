using System;
using UnityEngine;

public class BonusesWindowHider : MonoBehaviour
{
    private IOpenWindowButton _openWindowButton;
    private IReservedBonusesWindow _reservedBonusesWindow;

    private bool _wasButtonActive;

    private void OnEnable()
    {
        if (_openWindowButton == null || _reservedBonusesWindow == null)
            return;

        if (_wasButtonActive)
            _openWindowButton.Show();
    }

    private void OnDisable()
    {
        if (_openWindowButton == null || _reservedBonusesWindow == null)
            return;

        if (_openWindowButton.IsActive || _reservedBonusesWindow.IsActive)
            _wasButtonActive = true;
        else
            _wasButtonActive = false;

        if (_openWindowButton.IsActive)
            _openWindowButton.Hide();

        if (_reservedBonusesWindow.IsActive)
            _reservedBonusesWindow.Hide();
    }

    public void Initialize(IOpenWindowButton openWindowButton, IReservedBonusesWindow reservedBonusesWindow)
    {
        _openWindowButton = openWindowButton ?? throw new ArgumentNullException(nameof(openWindowButton));
        _reservedBonusesWindow = reservedBonusesWindow ?? throw new ArgumentNullException(nameof(reservedBonusesWindow));
        _wasButtonActive = false;
    }
}