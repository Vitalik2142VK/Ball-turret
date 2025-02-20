using System;

public class ActorsMoveStep : IStep, IEndPointStep
{
    private IEndStep _endStep;
    private IActorsMover _objectsMover;

    public ActorsMoveStep(IActorsMover objectsMover)
    {
        _objectsMover = objectsMover ?? throw new ArgumentNullException(nameof(objectsMover));
    }

    public void Action()
    {
        _objectsMover.MoveAll();

        if (_objectsMover.AreMovesFinished)
            _endStep.End();
    }

    public void SetEndStep(IEndStep endStep)
    {
        _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
    }
}
