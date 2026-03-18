using CannonTurret.Coin.Rewards;
using CannonTurret.SDK.Ads;
using CannonTurret.UI;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class RewardStep : IStep, IEndPointStep
    {
        private readonly IAdsViewer AdsViewer;
        private readonly IRewardIssuer RewardIssuer;

        private IEndStep _endStep;
        private IWindow _finishWindow;

        public RewardStep(IWindow finishWindow, IAdsViewer adsViewer, IRewardIssuer rewardIssuer)
        {
            _finishWindow = finishWindow ?? throw new ArgumentNullException(nameof(finishWindow));
            AdsViewer = adsViewer ?? throw new ArgumentNullException(nameof(adsViewer));
            RewardIssuer = rewardIssuer ?? throw new ArgumentNullException(nameof(rewardIssuer));
        }

        public void Action()
        {
            RewardIssuer.CalculateRevard();

            if (AdsViewer.IsAdsDisable || RewardIssuer.Reward == 0)
                RewardIssuer.PayMaxReward();
            else
                RewardIssuer.PayReward();

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
}