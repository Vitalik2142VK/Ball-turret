using System;

public class RewardStep : IStep, IEndPointStep
{
    private IEndStep _endStep;
    private IWindow _finishWindow;
    private IAdsViewer _adsViewer;
    private IRewardIssuer _rewardIssuer;

    public RewardStep(IWindow finishWindow, IAdsViewer adsViewer, IRewardIssuer rewardIssuer)
    {
        _finishWindow = finishWindow ?? throw new ArgumentNullException(nameof(finishWindow));
        _adsViewer = adsViewer ?? throw new ArgumentNullException(nameof(adsViewer));
        _rewardIssuer = rewardIssuer ?? throw new ArgumentNullException(nameof(rewardIssuer));
    }

    public void Action()
    {
        _rewardIssuer.CalculateRevard();

        if (_adsViewer.IsAdsDisable || _rewardIssuer.Reward == 0)
            _rewardIssuer.PayMaxReward();
        else
            _rewardIssuer.PayReward();

        _finishWindow.Enable();
        _endStep.End();
    }

    public void SetEndStep(IEndStep endStep)
    {
        _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
    }

    public void SetFinishWindow(IWindow finishWindow)
    {
        _finishWindow = finishWindow ?? throw new ArgumentNullException(nameof(finishWindow));
    }
}