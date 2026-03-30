using CannonTurret.Actors.Bonuses;
using CannonTurret.Actors.Bonuses.ReserveredBonuses;
using CannonTurret.UI.PlayerScene;
using System;
using System.Collections.Generic;

namespace CannonTurret.StepSystem.Steps
{
    public class BonusActivationStep : IStep, IEndPointStep
    {
        private readonly IBonusStorage _bonusStorage;
        private readonly IOpenWindowButton _openWindowButton;

        private IEndStep _endStep;
        private IBonusReservator _bonusReservator;
        private Queue<IBonus> _bonuses;

        public BonusActivationStep(IBonusStorage bonusStorage, IOpenWindowButton openWindowButton)
        {
            _bonusStorage = bonusStorage ?? throw new ArgumentNullException(nameof(bonusStorage));
            _openWindowButton = openWindowButton ?? throw new ArgumentNullException(nameof(openWindowButton));
        }

        public void Initialize(IBonusReservator bonusReservator)
        {
            _bonusReservator ??= bonusReservator ?? throw new ArgumentNullException(nameof(bonusReservator));
        }

        public void Action()
        {
            if (_bonuses == null || _bonuses.Count == 0)
            {
                if (_bonusStorage.TryGetBonuses(out IReadOnlyCollection<IBonus> bonuses))
                    _bonuses = new Queue<IBonus>(bonuses);
                else
                    FinishStep();
            }
            else
            {
                var bonus = _bonuses.Dequeue();

                if (_bonusReservator.TryAddBonusByName(bonus.BonusCard.Name) == false)
                    bonus.Activate();
            }
        }

        public void SetEndStep(IEndStep endStep)
        {
            _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
        }

        private void FinishStep()
        {
            _bonusReservator.Update();

            if (_bonusReservator.HasBonuses)
                _openWindowButton.Show();

            _endStep.End();
        }
    }
}