using System;
using System.Collections.Generic;

public class PurchasesStorage : IPurchasesStorage
{
    private Dictionary<string, IPlayerPurchase> _purchases;

    public PurchasesStorage(IEnumerable<IPlayerPurchase> purchases)
    {
        if (purchases == null)
            throw new ArgumentNullException(nameof(purchases));

        _purchases = new Dictionary<string, IPlayerPurchase>();

        foreach (var purchase in purchases)
            _purchases.Add(purchase.Id, purchase);
    }

    public bool TryGetPurchase(out IPlayerPurchase purchase, string id)
    {
        if (id == null || id.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(id));

        purchase = null;

        if (_purchases.ContainsKey(id))
        {
            purchase = _purchases[id];

            return true;
        }

        return false;
    }
}
