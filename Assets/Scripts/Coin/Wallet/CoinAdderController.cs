public interface IPurchaseRewardService
{
    public bool CanProvideReward(int fullPrice, int missingAmount);

    public void AssignReward(int missingAmount);
}
