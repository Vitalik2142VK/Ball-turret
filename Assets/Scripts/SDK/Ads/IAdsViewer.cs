using System;

public interface IAdsViewer
{
    public event Action<RewardType> RewardAdViewed;

    public bool CanShowRewardAd { get; }

    public void ShowRewardAd(RewardType reward);

    public void ShowFullScreenAd();
}
