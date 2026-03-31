using System;
using YG;

namespace CannonTurret.SDK.Shops
{
    public class OneTimePurchaseActivator : IPurchaseActivator
    {
        private readonly IPlayerPurchase _purchase;

        public OneTimePurchaseActivator(IPlayerPurchase purchase)
        {
            _purchase = purchase ?? throw new ArgumentNullException(nameof(purchase));
        }

        public void Activate(string purchaseId)
        {
            if (purchaseId == null)
                throw new ArgumentNullException(nameof(purchaseId));

            if (purchaseId != _purchase.Id)
                throw new ArgumentException($"The purchase ID - '{purchaseId}' does not match the activator ID - '{_purchase.Id}'");

            YG2.saves.ActivatePurchase(_purchase.Id);
            YG2.SaveProgress();

            _purchase.Update();
        }
    }
}