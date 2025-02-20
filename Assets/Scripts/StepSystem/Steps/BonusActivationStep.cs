using System;
using System.Collections.Generic;

public class BonusActivationStep : IStep, IEndPointStep
{
    private IEndStep _endStep;
    private IBonusStorage _bonusStorage;

    public BonusActivationStep(IBonusStorage bonusStorage)
    {
        _bonusStorage = bonusStorage ?? throw new ArgumentNullException(nameof(bonusStorage));
    }

    public void Action()
    {
        if (_bonusStorage.TryGetBonuses(out List<IBonus> bonuses))
        {
            ActiovateBonuses(bonuses);
        }
        else
        {
            _endStep.End();
        }
    }

    public void SetEndStep(IEndStep endStep)
    {
        _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
    }

    private void ActiovateBonuses(List<IBonus> bonuses)
    {
        foreach (var bonus in bonuses)
            bonus.Activate();

        _endStep.End();
    }
}
