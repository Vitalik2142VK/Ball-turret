public interface IRewardIssuer
{
    public bool IsRewardIssued { get; }

    public void AwardReward();

    public void ApplyMaxReward();

    public int GetReward();

    public int GetBonusReward();
}