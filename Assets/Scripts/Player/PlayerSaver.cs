using System;
using YG;

public class PlayerSaver : IPlayerSaver
{
    private IPlayer _player;
    private ISavesData _savesData;

    public PlayerSaver(IPlayer player, ISavesData savesData)
    {
        _player = player ?? throw new NullReferenceException(nameof(player));
        _savesData = savesData ?? throw new NullReferenceException(nameof(savesData));
    }

    public void Save()
    {
        _savesData.SetHealthCoefficient(_player.HealthCoefficient);
        _savesData.SetDamageCoefficient(_player.DamageCoefficient);
        _savesData.SetCountCoins(_player.Wallet.CountCoins);
        _savesData.SetAchievedLevelIndex(_player.AchievedLevelIndex);

        YandexGame.SaveProgress();
    }
}