using System;

public class NextStep : IEndStep
{
    private IStepSystem _stepSystem;
    private IStep _nextStep;

    private string _name_stepSystem;
    private string _name_nextStep;

    public NextStep(IStepSystem stepSystem, IStep nextStep)
    {
        _stepSystem = stepSystem ?? throw new ArgumentNullException(nameof(stepSystem));
        _nextStep = nextStep ?? throw new ArgumentNullException(nameof(nextStep));

        _name_stepSystem = stepSystem.GetType().Name;
        _name_nextStep = nextStep.GetType().Name;
    }

    public void End()
    {
        _stepSystem.EstablishNextStep(_nextStep);
    }
}
