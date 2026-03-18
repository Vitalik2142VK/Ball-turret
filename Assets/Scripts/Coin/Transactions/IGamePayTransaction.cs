using CannonTurret.Coin.Wallets;

namespace CannonTurret.Coin.Transactions
{
    public interface IGamePayTransaction
    {
        public int Price { get; }
        public bool IsLocked { get; }

        public bool TrySpend(IWallet wallet);

        public int GetMissingAmount();
    }
}