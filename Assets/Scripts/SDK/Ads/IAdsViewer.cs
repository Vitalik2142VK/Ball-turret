using System;

public interface IAdsViewer
{
    public event Action<string> RewardAdViewed;
    public event Action TimerRewardAdReseted;

    public bool CanShowRewardAd { get; }
    public bool IsAdsDisable { get; }

    public void ShowRewardAd(string rewardId);

    public void ShowFullScreenAd();
}
