using System;
using System.Collections.Generic;

public class BonusActivationStep : IStep, IEndPointStep
{
    private IEndStep _endStep;
    private IBonusStorage _bonusStorage;
    private IBonusReservator _bonusReservator;
    private IOpenWindowButton _openWindowButton;
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
        if (_bonusReservator.HasBonuses)
            _openWindowButton.Show();

        _bonusReservator.Update();
        _endStep.End();
    }
}
