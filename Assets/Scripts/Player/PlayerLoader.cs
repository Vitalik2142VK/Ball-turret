using System;

public class PlayerLoader : IPlayerLoader
{
    private IImprovementTurretAttributes _turretImproverAttributes;
    private ISavedPlayerData _savedData;
    private IPurchasesStorage _purchasesStorage;

    public PlayerLoader(IImprovementTurretAttributes turretImproverAttributes, ISavedPlayerData savedData)
    {
        _turretImproverAttributes = turretImproverAttributes ?? throw new ArgumentNullException(nameof(turretImproverAttributes));
        _savedData = savedData ?? throw new ArgumentNullException(nameof(savedData));
        _purchasesStorage = new PurchasesStorage(_savedData.OneTimePurchases);
    }

    public IPlayer Load()
    {
        if (_savedData.AchievedLevelIndex == 0)
            return CreateNewPlayer();
        else
            return GetFilledPlayer();
    }

    private IPlayer CreateNewPlayer()
    {
        Wallet wallet = new Wallet(_savedData.CountCoins);
        TurretImprover turretImprover = new TurretImprover(_turretImproverAttributes);

        return new Player(wallet, turretImprover, _purchasesStorage);
    }

    private IPlayer GetFilledPlayer()
    {
        float healthCoefficient = _savedData.HealthCoefficient;
        float damageCoefficient = _savedData.DamageCoefficient;
        Wallet wallet = new Wallet(_savedData.CountCoins);
        TurretImprover turretImprover = new TurretImprover(_turretImproverAttributes, healthCoefficient, damageCoefficient);

        return new Player(
            wallet,
            turretImprover,
            _purchasesStorage,
            _savedData.AchievedLevelIndex);
    }
}