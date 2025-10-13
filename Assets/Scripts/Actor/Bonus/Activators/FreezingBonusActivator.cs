using System;

public class FreezingBonusActivator : IBonusActivator
{
    private IDynamicEndStep _nextStepPrepareActors;
    private IStep _freezeStep;

    public FreezingBonusActivator(IDynamicEndStep nextStepPrepareActors, IStep freezeStep)
    {
        _nextStepPrepareActors = nextStepPrepareActors ?? throw new NullReferenceException(nameof(nextStepPrepareActors));
        _freezeStep = freezeStep ?? throw new NullReferenceException(nameof(freezeStep));

    }

    public void Activate()
    {
        _nextStepPrepareActors.SetNextStep(_freezeStep);
    }
}