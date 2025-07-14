using System;
using YG;

public class PlayerLoader : IPlayerLoader
{
    private IImprovementTurretAttributes _turretImproverAttributes;
    private ISavesData _savesData;

    public PlayerLoader(IImprovementTurretAttributes turretImproverAttributes, ISavesData savesData)
    {
        _turretImproverAttributes = turretImproverAttributes ?? throw new ArgumentNullException(nameof(turretImproverAttributes));
        _savesData = savesData ?? throw new ArgumentNullException(nameof(savesData));
    }

    public IPlayer Load()
    {
        if (_savesData.AchievedLevelIndex == 0)
            return CreateNewPlayer();
        else
            return GetFilledPlayer();
    }

    private IPlayer CreateNewPlayer()
    {
        Wallet wallet = new Wallet(_savesData.CountCoins);
        TurretImprover turretImprover = new TurretImprover(_turretImproverAttributes);
        Player newPlayer = new Player(turretImprover, wallet);

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
            turretImprover,
            wallet,
            _savesData.AchievedLevelIndex);
    }
}