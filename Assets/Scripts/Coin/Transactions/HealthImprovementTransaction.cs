using System;

public class HealthImprovementTransaction : IGamePayTransaction
{
    private IPlayerSaver _playerSaver;
    private IWallet _wallet;
    private ITurretImprover _turretImprover;
    private IPriceEnlarger _priceEnlarger;

    public HealthImprovementTransaction(IPlayerSaver playerSaver, IWallet wallet, ITurretImprover turretImprover, IPriceEnlarger priceEnlarger)
    {
        _playerSaver = playerSaver ?? throw new ArgumentNullException(nameof(playerSaver));
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
            _playerSaver.Save();

            return true;
        }

        return false;
    }

    public int GetMissingAmount()
    {
        if (IsLocked)
            return Price - (int)_wallet.CountCoins;
        else
            return 0;
    }
}
