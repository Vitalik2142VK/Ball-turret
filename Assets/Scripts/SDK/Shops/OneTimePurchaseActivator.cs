using System;
using YG;

namespace CannonTurret.SDK.Shops
{
    public class OneTimePurchaseActivator : IPurchaseActivator
    {
        private readonly IPlayerPurchase Purchase;

        public OneTimePurchaseActivator(IPlayerPurchase purchase)
        {
            Purchase = purchase ?? throw new ArgumentNullException(nameof(purchase));
        }

        public string PurchaseId => Purchase.Id;

        public void Activate(string purchaseId)
        {
            if (purchaseId == null)
                throw new ArgumentNullException(nameof(purchaseId));

            if (purchaseId != Purchase.Id)
                throw new ArgumentException($"The purchase ID - '{purchaseId}' does not match the activator ID - '{Purchase.Id}'");

            YG2.saves.ActivatePurchase(Purchase.Id);
            YG2.SaveProgress();

            Purchase.Update();
        }
    }
}