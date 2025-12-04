using System;

public class PlayVictoryEnemyStep : IStep, IEndPointStep
{
    private IEndStep _endStep;
    private IEnemiesVictory _enemiesVictory;

    public PlayVictoryEnemyStep(IEnemiesVictory enemiesVictory)
    {
        _enemiesVictory = enemiesVictory ?? throw new ArgumentNullException(nameof(enemiesVictory));
    }

    public void Action()
    {
        _enemiesVictory.WinAll();
        _endStep.End();
    }

    public void SetEndStep(IEndStep endStep)
    {
        _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
    }
}