using CannonTurret.Coin.Shops;
using CannonTurret.Coin.Wallets;
using CannonTurret.PlayerSystem;
using System;

namespace CannonTurret.Coin.Transactions
{
    public class HealthImprovementTransaction : IGamePayTransaction
    {
        private readonly IPlayerSaver PlayerSaver;
        private readonly IWallet Wallet;
        private readonly ITurretImprover TurretImprover;
        private readonly IPriceEnlarger PriceEnlarger;

        public HealthImprovementTransaction(IPlayerSaver playerSaver, IWallet wallet, ITurretImprover turretImprover, IPriceEnlarger priceEnlarger)
        {
            PlayerSaver = playerSaver ?? throw new ArgumentNullException(nameof(playerSaver));
            Wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            TurretImprover = turretImprover ?? throw new ArgumentNullException(nameof(turretImprover));
            PriceEnlarger = priceEnlarger ?? throw new ArgumentNullException(nameof(priceEnlarger));

            PriceEnlarger.IncreaseByLevel(TurretImprover.LevelHealthImprovement);
        }

        public int Price => PriceEnlarger.Price;

        public bool IsLocked => Price > Wallet.CountCoins;

        public bool TrySpend(IWallet wallet)
        {
            if (wallet == null)
                throw new ArgumentNullException(nameof(wallet));

            if (Wallet != wallet)
                return false;

            if (Wallet.TryPay(Price))
            {
                TurretImprover.ImproveHealth();
                PriceEnlarger.IncreaseByLevel(TurretImprover.LevelHealthImprovement);
                PlayerSaver.Save();

                return true;
            }

            return false;
        }

        public int GetMissingAmount()
        {
            if (IsLocked)
                return Price - (int)Wallet.CountCoins;
            else
                return 0;
        }
    }
}