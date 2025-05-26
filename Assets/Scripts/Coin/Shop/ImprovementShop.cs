using System;
using System.Collections.Generic;

public class ImprovementShop : IImprovementShop
{
    private Dictionary<Type, IGamePayTransaction> _transactions;
    private Dictionary<Type, IImprovementProduct> _products;
    private IWallet _wallet;

    public ImprovementShop(IWallet wallet, IEnumerable<IGamePayTransaction> transactions, IEnumerable<IImprovementProduct> products)
    {
        _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
        _transactions = AddTransactions(transactions);
        _products = AddProducts(products);
    }

    public bool TryMakeTransaction(IGamePayTransaction transaction)
    {
        if (transaction == null) 
            throw new ArgumentNullException(nameof(transaction));

        if (transaction.Price > _wallet.CountCoins)
            return false;

        return transaction.TrySpend(_wallet);
    }

    public IGamePayTransaction GetTransaction(Type type)
    {
        if (_transactions.ContainsKey(type) == false)
            throw new ArgumentOutOfRangeException(nameof(type));

        return _transactions[type];
    }

    public IImprovementProduct GetProduct(Type type)
    {
        if (_products.ContainsKey(type) == false)
            throw new ArgumentOutOfRangeException(nameof(type));

        return _products[type];
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
