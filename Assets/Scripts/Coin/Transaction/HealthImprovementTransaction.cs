using System;

public class HealthImprovementTransaction : IGamePayTransaction
{
    private IWallet _wallet;
    private ITurretImprover _turretImprover;
    private IPriceEnlarger _priceEnlarger;

    public HealthImprovementTransaction(IWallet wallet, ITurretImprover turretImprover, IPriceEnlarger priceEnlarger)
    {
        _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
        _turretImprover = turretImprover ?? throw new ArgumentNullException(nameof(turretImprover));
        _priceEnlarger = priceEnlarger ?? throw new ArgumentNullException(nameof(priceEnlarger));

        _priceEnlarger.IncreaseByLevel(_turretImprover.LevelHealthImprovement);
    }

    public int Price => _priceEnlarger.Price;
    public bool IsLocked => Price > _wallet.CountCoins;

    public bool TrySpend(IWallet wallet)
    {
        if (wallet == null)
            throw new ArgumentNullException(nameof(wallet));

        if (_wallet != wallet)
            return false;

        if (_wallet.TryPay(Price))
        {
            _turretImprover.ImproveHealth();
            _priceEnlarger.IncreaseByLevel(_turretImprover.LevelHealthImprovement);

            return true;
        }

        return false;
    }
}
