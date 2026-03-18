using CannonTurret.Coin.Products;
using CannonTurret.Coin.Transactions;
using System;

namespace CannonTurret.Coin.Shops
{
    public interface IImprovementShop
    {
        public bool TryMakeTransaction(IGamePayTransaction transaction);

        public IGamePayTransaction GetTransaction(Type type);

        public IImprovementProduct GetProduct(Type type);
    }
}