using System;

public class PurchaseRewardService : IPurchaseRewardService
{
    private const float MaxPriceCoefficient = 1.0f;

    private ICoinAdder _coinAdder;
    private float _priceCoefficient;

    public PurchaseRewardService(ICoinAdder coinAdder, float priceCoefficient = MaxPriceCoefficient)
    {
        if (priceCoefficient < 0.0f || priceCoefficient > MaxPriceCoefficient)
            throw new ArgumentOutOfRangeException(nameof(priceCoefficient));

        _coinAdder = coinAdder ?? throw new ArgumentNullException(nameof(coinAdder));
        _priceCoefficient = priceCoefficient;
    }

    public bool CanProvideReward(int fullPrice, int missingAmount)
    {
        if (fullPrice < 0 || fullPrice < missingAmount)
            throw new ArgumentOutOfRangeException(nameof(fullPrice));

        if (missingAmount < 0)
            throw new ArgumentOutOfRangeException(nameof(missingAmount));

        int maxReward = (int)(fullPrice * _priceCoefficient);

        return missingAmount <= maxReward;
    }

    public void AssignReward(int missingAmount)
    {
        if (missingAmount < 0)
            throw new ArgumentOutOfRangeException(nameof(missingAmount));

        _coinAdder.SetCoinsAdsView(missingAmount);
    }
}