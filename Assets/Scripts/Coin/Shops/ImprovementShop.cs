using CannonTurret.Coin.Products;
using CannonTurret.Coin.Transactions;
using CannonTurret.Coin.Wallets;
using System;
using System.Collections.Generic;

namespace CannonTurret.Coin.Shops
{
    public class ImprovementShop : IImprovementShop
    {
        private readonly Dictionary<Type, IGamePayTransaction> Transactions;
        private readonly Dictionary<Type, IImprovementProduct> Products;
        private readonly IWallet Wallet;

        public ImprovementShop(IWallet wallet, IEnumerable<IGamePayTransaction> transactions, IEnumerable<IImprovementProduct> products)
        {
            Wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            Transactions = AddTransactions(transactions);
            Products = AddProducts(products);
        }

        public bool TryMakeTransaction(IGamePayTransaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            if (transaction.Price > Wallet.CountCoins)
                return false;

            return transaction.TrySpend(Wallet);
        }

        public IGamePayTransaction GetTransaction(Type type)
        {
            if (Transactions.ContainsKey(type) == false)
                throw new ArgumentOutOfRangeException(nameof(type));

            return Transactions[type];
        }

        public IImprovementProduct GetProduct(Type type)
        {
            if (Products.ContainsKey(type) == false)
                throw new ArgumentOutOfRangeException(nameof(type));

            return Products[type];
        }

        private Dictionary<Type, IGamePayTransaction> AddTransactions(IEnumerable<IGamePayTransaction> transactions)
        {
            if (transactions == null)
                throw new ArgumentNullException(nameof(transactions));

            Dictionary<Type, IGamePayTransaction> addedTransactions = new Dictionary<Type, IGamePayTransaction>();

            foreach (var transaction in transactions)
                addedTransactions.Add(transaction.GetType(), transaction);

            if (addedTransactions.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(transactions));

            return addedTransactions;
        }

        private Dictionary<Type, IImprovementProduct> AddProducts(IEnumerable<IImprovementProduct> products)
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            Dictionary<Type, IImprovementProduct> addedProducts = new Dictionary<Type, IImprovementProduct>();

            foreach (var transaction in products)
                addedProducts.Add(transaction.GetType(), transaction);

            if (addedProducts.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(products));

            return addedProducts;
        }
    }
}