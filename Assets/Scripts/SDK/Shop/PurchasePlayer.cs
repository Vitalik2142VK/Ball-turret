using System;
using YG.Utils.Pay;

public class PurchasePlayer : IPurchase
{
    private Purchase _purchase;

    public PurchasePlayer(Purchase purchase)
    {
        _purchase = purchase ?? throw new ArgumentNullException(nameof(purchase));
    }

    public string Id => _purchase.id;
    public bool IsConsumed => _purchase.consumed;
}
