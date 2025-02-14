using System.Collections.Generic;
using UnityEngine;

public class BonusActivationStep : IStep
{
    private IEndStep _endStep;
    private IBonusStorage _bonusStorage;

    public BonusActivationStep(IEndStep endStep, IBonusStorage bonusStorage)
    {
        _endStep = endStep ?? throw new System.ArgumentNullException(nameof(endStep));
        _bonusStorage = bonusStorage ?? throw new System.ArgumentNullException(nameof(bonusStorage));
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

    private void ActiovateBonuses(List<IBonus> bonuses)
    {
        foreach (var bonus in bonuses)
            bonus.Activate();

        _endStep.End();
    }
}