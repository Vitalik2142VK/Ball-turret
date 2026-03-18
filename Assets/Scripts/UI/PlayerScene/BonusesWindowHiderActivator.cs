using CannonTurret.Turrets;
using System;

namespace CannonTurret.UI.PlayerScene
{
    public class BonusesWindowHiderActivator
    {
        private readonly IOpenWindowButton OpenWindowButton;
        private readonly IReservedBonusesWindow ReservedBonusesWindow;
        private readonly IShotAction ShotAction;

        public BonusesWindowHiderActivator(IOpenWindowButton openWindowButton, IReservedBonusesWindow reservedBonusesWindow, IShotAction shotAction)
        {
            OpenWindowButton = openWindowButton ?? throw new ArgumentNullException(nameof(openWindowButton));
            ReservedBonusesWindow = reservedBonusesWindow ?? throw new ArgumentNullException(nameof(reservedBonusesWindow));
            ShotAction = shotAction ?? throw new ArgumentNullException(nameof(shotAction));

            ShotAction.Fired += OnHide;
        }

        public void Disable()
        {
            ShotAction.Fired -= OnHide;
        }

        private void OnHide()
        {
            if (OpenWindowButton.IsActive)
                OpenWindowButton.Hide();

            if (ReservedBonusesWindow.IsActive)
                ReservedBonusesWindow.Hide();
        }
    }
}