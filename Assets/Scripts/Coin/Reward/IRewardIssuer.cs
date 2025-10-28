public interface IRewardIssuer
{
    public bool IsRewardIssued { get; }

    public void PayReward();

    public void PayMaxReward();

    public int GetReward();

    public int GetMaxReward();
}