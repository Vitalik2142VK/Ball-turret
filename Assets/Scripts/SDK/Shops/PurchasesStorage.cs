using System;
using System.Collections.Generic;

namespace CannonTurret.SDK.Shops
{
    public class PurchasesStorage : IPurchasesStorage
    {
        private readonly Dictionary<string, IPlayerPurchase> Purchases;

        public PurchasesStorage(IEnumerable<IPlayerPurchase> purchases)
        {
            if (purchases == null)
                throw new ArgumentNullException(nameof(purchases));

            Purchases = new Dictionary<string, IPlayerPurchase>();

            foreach (var purchase in purchases)
                Purchases.Add(purchase.Id, purchase);
        }

        public bool TryGetPurchase(out IPlayerPurchase purchase, string id)
        {
            if (id == null || id.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            purchase = null;

            if (Purchases.ContainsKey(id))
            {
                purchase = Purchases[id];

                return true;
            }

            return false;
        }
    }
}