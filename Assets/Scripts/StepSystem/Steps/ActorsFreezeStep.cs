using System;

public class ActorsFreezeStep : IStep, IEndPointStep
{
    private IEndStep _endStep;
    private IDynamicEndStep _nextStepPrepareActors;
    private IActorsFreezerView _freezer;
    private IStep _interruptedStep;

    public ActorsFreezeStep(IDynamicEndStep nextStepPrepareActors, IStep interruptedStep, IActorsFreezerView freezer)
    {
        _nextStepPrepareActors = nextStepPrepareActors ?? throw new NullReferenceException(nameof(nextStepPrepareActors));
        _interruptedStep = interruptedStep ?? throw new NullReferenceException(nameof(interruptedStep));
        _freezer = freezer ?? throw new NullReferenceException(nameof(freezer));
    }

    public void Action()
    {
        _nextStepPrepareActors.SetNextStep(_interruptedStep);
        _freezer.Defrost();
        _endStep.End();
    }

    public void SetEndStep(IEndStep endStep)
    {
        _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
    }
}