using System;

public class FreezingBonusActivator : IBonusActivator
{
    private IDynamicEndStep _nextStepPrepareActors;
    private IBonusActicatorView _view;
    private IStep _freezeStep;

    public FreezingBonusActivator(IDynamicEndStep nextStepPrepareActors, IBonusActicatorView view, IStep freezeStep)
    {
        _nextStepPrepareActors = nextStepPrepareActors ?? throw new NullReferenceException(nameof(nextStepPrepareActors));
        _view = view ?? throw new NullReferenceException(nameof(view));
        _freezeStep = freezeStep ?? throw new NullReferenceException(nameof(freezeStep));

    }

    public void Activate()
    {
        _nextStepPrepareActors.SetNextStep(_freezeStep);
        _view.PlayActivation();
    }
}