using System;
using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        public List<OneTimePurchase> Purchases;

        public SavesYG()
        {
            Purchases = new List<OneTimePurchase>()
            {
                new OneTimePurchase(PurchasesTypes.DisableAds)
            };
        }

        public List<IPlayerPurchase> GetOneTimePurchases()
        {
            List<IPlayerPurchase> purchases = new List<IPlayerPurchase>(Purchases.Count);

            foreach (var purchase in Purchases)
                purchases.Add(new PlayerPurchase(purchase));

            return purchases;
        }

        public OneTimePurchase GetOneTimePurchase(string purchseId)
        {
            foreach (var purchase in Purchases)
                if (purchase.Id == purchseId)
                    return purchase;

            throw new InvalidOperationException($"purchase with ID {purchseId} not found");
        }

        public void ActivatePurchase(string purchseId)
        {
            for (int i = 0; i < Purchases.Count; i++)
            {
                if (Purchases[i].Id == purchseId)
                {
                    var purchase = Purchases[i];
                    purchase.IsPurchased = true;
                    Purchases[i] = purchase;

                    break;
                }
            }
        }

        public void CheckPurchaseAvailability()
        {
            bool isValid = true;
            string listIds = "";

            foreach (var purchase in Purchases)
            {
                if (YG2.PurchaseByID(purchase.Id) == null)
                {
                    isValid = false;
                    listIds += $"{purchase.Id};\n";
                }
            }

            if (isValid == false)
                throw new InvalidOperationException($"Purchases with the following IDs were not found:\n{listIds}");
        }
    }
}