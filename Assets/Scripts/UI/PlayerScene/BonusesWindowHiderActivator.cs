using System;

public class BonusesWindowHiderActivator
{
    private IOpenWindowButton _openWindowButton;
    private IReservedBonusesWindow _reservedBonusesWindow;
    private IShotAction _shotAction;

    public BonusesWindowHiderActivator(IOpenWindowButton openWindowButton, IReservedBonusesWindow reservedBonusesWindow, IShotAction shotAction)
    {
        _openWindowButton = openWindowButton ?? throw new ArgumentNullException(nameof(openWindowButton));
        _reservedBonusesWindow = reservedBonusesWindow ?? throw new ArgumentNullException(nameof(reservedBonusesWindow));
        _shotAction = shotAction ?? throw new ArgumentNullException(nameof(shotAction));

        _shotAction.Fired += OnHide;
    }

    public void Disable()
    {
        _shotAction.Fired -= OnHide;
    }

    private void OnHide()
    {
        if (_openWindowButton.IsActive)
            _openWindowButton.Hide();

        if (_reservedBonusesWindow.IsActive)
            _reservedBonusesWindow.Hide();
    }
}