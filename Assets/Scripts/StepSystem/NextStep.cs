using System;

public class NextStep : IEndStep
{
    private IStepSystem _stepSystem;
    private IStep _nextStep;

    public NextStep(IStepSystem stepSystem, IStep nextStep)
    {
        _stepSystem = stepSystem ?? throw new ArgumentNullException(nameof(stepSystem));
        _nextStep = nextStep ?? throw new ArgumentNullException(nameof(nextStep));
    }

    public void End()
    {
        _stepSystem.EstablishNextStep(_nextStep);
    }
}
