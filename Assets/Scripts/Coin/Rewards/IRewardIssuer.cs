namespace CannonTurret.Coin.Rewards
{
    public interface IRewardIssuer : IRewardData
    {
        public void PayReward();

        public void PayMaxReward();

        public void CalculateRevard();
    }
}