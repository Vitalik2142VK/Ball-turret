using CannonTurret.SDK.Shops;
using System;
using System.Collections.Generic;
using YG;

namespace CannonTurret.PlayerSystem
{
    public class SavedPlayerData : ISavedPlayerData
    {
        private readonly SavesYG SavesYG;

        public SavedPlayerData()
        {
            if (YG2.isSDKEnabled == false)
                throw new InvalidOperationException("The Yandex SDK is not Enabled");

            SavesYG = YG2.saves;
            SavesYG.CheckPurchaseAvailability();
        }

        public IReadOnlyCollection<IPlayerPurchase> OneTimePurchases => SavesYG.GetOneTimePurchases();
        public float HealthCoefficient => SavesYG.HealthCoefficient;
        public float DamageCoefficient => SavesYG.DamageCoefficient;
        public long CountCoins => SavesYG.CountCoins;
        public int AchievedLevelIndex => SavesYG.AchievedLevelIndex;

        public void SetHealthCoefficient(float healthCoefficient)
        {
            if (healthCoefficient < SavesYG.HealthCoefficient)
                throw new ArgumentOutOfRangeException(nameof(healthCoefficient));

            SavesYG.HealthCoefficient = healthCoefficient;
        }

        public void SetDamageCoefficient(float damageCoefficient)
        {
            if (damageCoefficient < SavesYG.DamageCoefficient)
                throw new ArgumentOutOfRangeException(nameof(damageCoefficient));

            SavesYG.DamageCoefficient = damageCoefficient;
        }

        public void SetCountCoins(long countCoins)
        {
            if (countCoins < 0)
                throw new ArgumentOutOfRangeException(nameof(countCoins));

            SavesYG.CountCoins = countCoins;
        }

        public void SetAchievedLevelIndex(int achievedLevelIndex)
        {
            int nextLevelIndex = 1;
            nextLevelIndex += SavesYG.AchievedLevelIndex;

            if (achievedLevelIndex > nextLevelIndex || achievedLevelIndex < SavesYG.AchievedLevelIndex)
                throw new ArgumentOutOfRangeException(nameof(achievedLevelIndex));

            SavesYG.AchievedLevelIndex = achievedLevelIndex;
        }

        public void RemoveProgerss()
        {
            SavesYG clearSave = new SavesYG();
            SavesYG.HealthCoefficient = clearSave.HealthCoefficient;
            SavesYG.DamageCoefficient = clearSave.DamageCoefficient;
            SavesYG.CountCoins = clearSave.CountCoins;
            SavesYG.AchievedLevelIndex = clearSave.AchievedLevelIndex;

            YG2.SaveProgress();
        }

        public void RemoveAll()
        {
            YG2.saves = new SavesYG();
            YG2.SaveProgress();
        }
    }
}