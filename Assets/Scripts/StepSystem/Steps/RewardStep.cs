using System;

public class RewardStep : IStep, IEndPointStep
{
    private IEndStep _endStep;
    private IWindow _finishWindow;

    public RewardStep(IWindow finishWindow)
    {
        _finishWindow = finishWindow ?? throw new ArgumentNullException(nameof(finishWindow));
    }

    public void Action()
    {
        _finishWindow.Enable();
        _endStep.End();
    }

    public void SetEndStep(IEndStep endStep)
    {
        _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
    }
}