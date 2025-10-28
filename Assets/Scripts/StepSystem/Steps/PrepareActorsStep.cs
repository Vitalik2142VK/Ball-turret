using System;

public class PrepareActorsStep : IStep, IEndPointStep
{
    private IEndStep _endStep;
    private IActorsPreparator _actorsPreparator;

    public PrepareActorsStep(IActorsPreparator actorsPreparator)
    {
        _actorsPreparator = actorsPreparator ?? throw new ArgumentNullException(nameof(actorsPreparator));
    }

    public void Action()
    {
        _actorsPreparator.Prepare();
        _endStep.End();
    }

    public void SetEndStep(IEndStep endStep)
    {
        _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
    }
}
