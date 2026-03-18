using CannonTurret.Actors.Bonuses.ReserveredBonuses;
using CannonTurret.UI.PlayerScene;
using System;

namespace CannonTurret.Actors.Bonuses.Activators
{
    public class ChoiceBonusActivator : IBonusActivator
    {
        private readonly IBonusChoiceMenu ChoiceBonusMenu;
        private readonly IBonusRandomizer BonusRandomizer;

        private IBonusReservator _bonusReservator;
        private bool _isActive;

        public ChoiceBonusActivator(IBonusChoiceMenu choiceBonusMenu, IBonusRandomizer bonusRandomizer)
        {
            ChoiceBonusMenu = choiceBonusMenu ?? throw new ArgumentNullException(nameof(choiceBonusMenu));
            BonusRandomizer = bonusRandomizer ?? throw new ArgumentNullException(nameof(bonusRandomizer));
            _isActive = false;
        }

        public void Activate()
        {
            if (_isActive)
                throw new InvalidOperationException($"{nameof(ChoiceBonusActivator)} is already active.");

            _isActive = true;

            ChoiceBonusMenu.BonusSelected += OnHandleBonus;
            ChoiceBonusMenu.SetBonusRandomizer(BonusRandomizer);
            ChoiceBonusMenu.Enable();
        }

        public void SetBonusReservator(IBonusReservator bonusReservator)
        {
            _bonusReservator = bonusReservator ?? throw new ArgumentNullException(nameof(bonusReservator));
        }

        private void OnHandleBonus()
        {
            ChoiceBonusMenu.BonusSelected -= OnHandleBonus;
            _isActive = false;

            var bonus = ChoiceBonusMenu.SelectedBonus;

            if (_bonusReservator == null || _bonusReservator.TryAddBonusByName(bonus.BonusCard.Name) == false)
                bonus.Activate();
        }
    }
}