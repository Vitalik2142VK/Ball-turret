using System;

public class RemoveActorsStep : IStep, IEndPointStep
{
    private IEndStep _endStep;
    private IActorsRemover _actorsRemover;

    public RemoveActorsStep(IActorsRemover actorsRemover)
    {
        _actorsRemover = actorsRemover ?? throw new ArgumentNullException(nameof(actorsRemover));
    }

    public void Action()
    {
        _actorsRemover.RemoveAllDisabled();
        _endStep.End();
    }

    public void SetEndStep(IEndStep endStep)
    {
        _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
    }
}