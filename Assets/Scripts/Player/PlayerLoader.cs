using System;
using YG;

public class PlayerLoader : IPlayerLoader
{
    private IImprovementTurretAttributes _turretImproverAttributes;
    private ISavesData _savesData;
    private IPurchasesStorage _purchasesStorage;

    public PlayerLoader(IImprovementTurretAttributes turretImproverAttributes, ISavesData savesData, IPurchasesStorage purchasesStorage)
    {
        _turretImproverAttributes = turretImproverAttributes ?? throw new ArgumentNullException(nameof(turretImproverAttributes));
        _savesData = savesData ?? throw new ArgumentNullException(nameof(savesData));
        _purchasesStorage = purchasesStorage ?? throw new ArgumentNullException(nameof(purchasesStorage));
    }

    public IPlayer Load()
    {
        if (_savesData.AchievedLevelIndex == 0)
            return CreateNewPlayer();
        else
            return GetFilledPlayer();
    }

    public void UpdatePurchasesStorage(IPlayer player)
    {
        if (player is Player savedPlayer)
            savedPlayer.SetPurchasesStorage(_purchasesStorage);
    }

    private IPlayer CreateNewPlayer()
    {
        Wallet wallet = new Wallet(_savesData.CountCoins);
        TurretImprover turretImprover = new TurretImprover(_turretImproverAttributes);
        Player newPlayer = new Player(wallet, turretImprover, _purchasesStorage);

        _savesData.SetHealthCoefficient(newPlayer.HealthCoefficient);
        _savesData.SetDamageCoefficient(newPlayer.DamageCoefficient);
        _savesData.SetAchievedLevelIndex(newPlayer.AchievedLevelIndex);

        YandexGame.SaveProgress();

        return newPlayer;
    }

    private IPlayer GetFilledPlayer()
    {
        float healthCoefficient = _savesData.HealthCoefficient;
        float damageCoefficient = _savesData.DamageCoefficient;
        Wallet wallet = new Wallet(_savesData.CountCoins);
        TurretImprover turretImprover = new TurretImprover(_turretImproverAttributes, healthCoefficient, damageCoefficient);

        return new Player(
            wallet,
            turretImprover,
            _purchasesStorage,
            _savesData.AchievedLevelIndex);
    }
}