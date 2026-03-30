using CannonTurret.Turrets;
using System;

namespace CannonTurret.UI.PlayerScene
{
    public class BonusesWindowHiderActivator
    {
        private readonly IOpenWindowButton _openWindowButton;
        private readonly IReservedBonusesWindow _reservedBonusesWindow;
        private readonly IShotAction _shotAction;

        public BonusesWindowHiderActivator(
            IOpenWindowButton openWindowButton,
            IReservedBonusesWindow reservedBonusesWindow,
            IShotAction shotAction)
        {
            if (openWindowButton == null)
                throw new ArgumentNullException(nameof(openWindowButton));

            if (reservedBonusesWindow == null)
                throw new ArgumentNullException(nameof(reservedBonusesWindow));

            if (openWindowButton == null)
                throw new ArgumentNullException(nameof(shotAction));

            _openWindowButton = openWindowButton;
            _reservedBonusesWindow = reservedBonusesWindow;
            _shotAction = shotAction;

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
}