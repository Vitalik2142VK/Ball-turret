using System;

namespace CannonTurret.Coin.Wallets
{
    public class PurchaseRewardService : IPurchaseRewardService
    {
        private const float MaxPriceCoefficient = 1.0f;

        private readonly ICoinAdder CoinAdder;
        private readonly float PriceCoefficient;

        public PurchaseRewardService(ICoinAdder coinAdder, float priceCoefficient = MaxPriceCoefficient)
        {
            if (priceCoefficient < 0.0f || priceCoefficient > MaxPriceCoefficient)
                throw new ArgumentOutOfRangeException(nameof(priceCoefficient));

            CoinAdder = coinAdder ?? throw new ArgumentNullException(nameof(coinAdder));
            PriceCoefficient = priceCoefficient;
        }

        public bool CanProvideReward(int fullPrice, int missingAmount)
        {
            if (fullPrice < 0 || fullPrice < missingAmount)
                throw new ArgumentOutOfRangeException(nameof(fullPrice));

            if (missingAmount < 0)
                throw new ArgumentOutOfRangeException(nameof(missingAmount));

            int maxReward = (int)(fullPrice * PriceCoefficient);

            return missingAmount <= maxReward;
        }

        public void AssignReward(int missingAmount)
        {
            if (missingAmount < 0)
                throw new ArgumentOutOfRangeException(nameof(missingAmount));

            CoinAdder.SetCoinsAdsView(missingAmount);
        }
    }
}