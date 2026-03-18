using CannonTurret.Coin.Wallets;
using CannonTurret.SDK.Shops;

namespace CannonTurret.PlayerSystem
{
    public interface IPlayer
    {
        public IWallet Wallet { get; }
        public ITurretImprover TurretImprover { get; }
        public IPurchasesStorage PurchasesStorage { get; }
        public float HealthCoefficient { get; }
        public float DamageCoefficient { get; }
        public int AchievedLevelIndex { get; }
        public bool IsLearningComplete { get; }

        public void IncreaseAchievedLevel();
    }
}