using System;

public class EnemyAttackStep : IStep, IEndPointStep
{
    private IEndStep _endStep;
    private IEnemiesAttacker _enemiesAttacker;

    public EnemyAttackStep(IEnemiesAttacker enemiesAttacker)
    {
        _enemiesAttacker = enemiesAttacker ?? throw new ArgumentNullException(nameof(enemiesAttacker));
    }

    public void Action()
    {
        _enemiesAttacker.AttackAll();
        _endStep.End();
    }

    public void SetEndStep(IEndStep endStep)
    {
        _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
    }
}
