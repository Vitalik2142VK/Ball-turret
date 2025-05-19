using System;

public class DynamicNextStep : IDynamicEndStep
{
    private IStepSystem _stepSystem;
    private IStep _nextStep;

    public DynamicNextStep(IStepSystem stepSystem)
    {
        _stepSystem = stepSystem ?? throw new ArgumentNullException(nameof(stepSystem));
    }

    public void SetNextStep(IStep nextStep)
    {
        _nextStep = nextStep ?? throw new ArgumentNullException(nameof(nextStep));
    }

    public void End()
    {
        _stepSystem.EstablishNextStep(_nextStep);
    }
}