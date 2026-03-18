using CannonTurret.Coin.Wallets;
using CannonTurret.SDK.Shops;
using System;

namespace CannonTurret.PlayerSystem
{
    public class PlayerLoader : IPlayerLoader
    {
        private readonly IImprovementTurretAttributes TurretImproverAttributes;
        private readonly ISavedPlayerData SavedData;
        private readonly IPurchasesStorage PurchasesStorage;

        public PlayerLoader(IImprovementTurretAttributes turretImproverAttributes, ISavedPlayerData savedData)
        {
            TurretImproverAttributes = turretImproverAttributes ?? throw new ArgumentNullException(nameof(turretImproverAttributes));
            SavedData = savedData ?? throw new ArgumentNullException(nameof(savedData));
            PurchasesStorage = new PurchasesStorage(SavedData.OneTimePurchases);
        }

        public IPlayer Load()
        {
            if (SavedData.AchievedLevelIndex == 0)
                return CreateNewPlayer();
            else
                return GetFilledPlayer();
        }

        private IPlayer CreateNewPlayer()
        {
            Wallet wallet = new Wallet(SavedData.CountCoins);
            TurretImprover turretImprover = new TurretImprover(TurretImproverAttributes);

            return new Player(wallet, turretImprover, PurchasesStorage);
        }

        private IPlayer GetFilledPlayer()
        {
            float healthCoefficient = SavedData.HealthCoefficient;
            float damageCoefficient = SavedData.DamageCoefficient;
            Wallet wallet = new Wallet(SavedData.CountCoins);
            TurretImprover turretImprover = new TurretImprover(TurretImproverAttributes, healthCoefficient, damageCoefficient);

            return new Player(
                wallet,
                turretImprover,
                PurchasesStorage,
                SavedData.AchievedLevelIndex);
        }
    }
}