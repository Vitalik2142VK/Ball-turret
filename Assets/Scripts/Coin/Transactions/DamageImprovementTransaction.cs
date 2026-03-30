using CannonTurret.Coin.Shops;
using CannonTurret.Coin.Wallets;
using CannonTurret.PlayerSystem;
using System;

namespace CannonTurret.Coin.Transactions
{
    public class DamageImprovementTransaction : IGamePayTransaction
    {
        private readonly IPlayerSaver _playerSaver;
        private readonly IWallet _wallet;
        private readonly ITurretImprover _turretImprover;
        private readonly IPriceEnlarger _priceEnlarger;

        public DamageImprovementTransaction(
            IPlayerSaver playerSaver,
            IWallet wallet,
            ITurretImprover turretImprover,
            IPriceEnlarger priceEnlarger)
        {
            _playerSaver = playerSaver ?? throw new ArgumentNullException(nameof(playerSaver));
            _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            _turretImprover = turretImprover ?? throw new ArgumentNullException(nameof(turretImprover));
            _priceEnlarger = priceEnlarger ?? throw new ArgumentNullException(nameof(priceEnlarger));

            _priceEnlarger.IncreaseByLevel(_turretImprover.GetLevelDamageImprovement());
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
                _turretImprover.ImproveDamage();
                _priceEnlarger.IncreaseByLevel(_turretImprover.GetLevelDamageImprovement());
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
}