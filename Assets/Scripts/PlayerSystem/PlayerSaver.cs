using System;
using YG;

namespace CannonTurret.PlayerSystem
{
    public class PlayerSaver : IPlayerSaver
    {
        private readonly IPlayer Player;
        private readonly ISavedPlayerData SavesData;

        public PlayerSaver(IPlayer player, ISavedPlayerData savesData)
        {
            Player = player ?? throw new NullReferenceException(nameof(player));
            SavesData = savesData ?? throw new NullReferenceException(nameof(savesData));
        }

        public void Save()
        {
            SavesData.SetHealthCoefficient(Player.HealthCoefficient);
            SavesData.SetDamageCoefficient(Player.DamageCoefficient);
            SavesData.SetCountCoins(Player.Wallet.CountCoins);
            SavesData.SetAchievedLevelIndex(Player.AchievedLevelIndex);

            YG2.SaveProgress();
        }
    }
}