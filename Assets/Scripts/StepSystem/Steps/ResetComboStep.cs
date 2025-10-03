using System;

public class ResetComboStep : IStep, IEndPointStep
{
    private IEndStep _endStep;
    private IComboCounterResetter _resetter;

    public ResetComboStep(IComboCounterResetter resetter)
    {
        _resetter = resetter ?? throw new ArgumentNullException(nameof(resetter));
    }

    public void Action()
    {
        _resetter.ResetCombo();
        _endStep.End();
    }

    public void SetEndStep(IEndStep endStep)
    {
        _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
    }
}