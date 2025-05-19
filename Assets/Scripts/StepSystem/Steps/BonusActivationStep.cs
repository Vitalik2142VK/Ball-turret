using System;
using System.Collections.Generic;

public class BonusActivationStep : IStep, IEndPointStep
{
    private IEndStep _endStep;
    private IBonusStorage _bonusStorage;
    private Queue<IBonus> _bonuses;

    public BonusActivationStep(IBonusStorage bonusStorage)
    {
        _bonusStorage = bonusStorage ?? throw new ArgumentNullException(nameof(bonusStorage));
    }

    public void Action()
    {
        if (_bonuses == null || _bonuses.Count == 0)
            if (_bonusStorage.TryGetBonuses(out IReadOnlyCollection<IBonus> bonuses))
                _bonuses = new Queue<IBonus>(bonuses);
            else
                _endStep.End();
        else
            ActiovateBonuses();
    }

    public void SetEndStep(IEndStep endStep)
    {
        _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
    }

    private void ActiovateBonuses()
    {
        var bonus = _bonuses.Dequeue();
        bonus.Activate();
    }
}
