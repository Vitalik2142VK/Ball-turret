using System;
using YG;

public class OneTimePurchaseActivator : IPurchaseActivator
{
    private IPlayerPurchase _purchase;

    public OneTimePurchaseActivator(IPlayerPurchase purchase)
    {
        _purchase = purchase ?? throw new ArgumentNullException(nameof(purchase));
    }

    public string PurchaseId => _purchase.Id;

    public void Activate(string purchaseId)
    {
        if (purchaseId == null)
            throw new ArgumentNullException(nameof(purchaseId));

        if (purchaseId != _purchase.Id)
            throw new InvalidOperationException($"The purchase ID - '{purchaseId}' does not match the activator ID - '{_purchase.Id}'");

        YG2.saves.ActivatePurchase(_purchase.Id);
        YG2.SaveProgress();

        _purchase.Update();
    }
}
