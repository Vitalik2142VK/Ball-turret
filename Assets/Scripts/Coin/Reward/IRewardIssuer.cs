public interface IRewardIssuer
{
    public bool IsRewardIssued { get; }
    public bool IsBonusRewardIssued { get; }

    public void AwardReward();

    public void AwarBonusReward();

    public int GetReward();

    public int GetBonusReward();

    public int GetMaxReward();
}