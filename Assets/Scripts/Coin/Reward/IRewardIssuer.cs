public interface IRewardIssuer
{
    public bool IsRewardIssued { get; }
    public bool IsBonusRewardIssued { get; }

    public void PayReward();

    public void PayBonusReward();

    public void PayMaxReward();

    public int GetReward();

    public int GetBonusReward();

    public int GetMaxReward();
}