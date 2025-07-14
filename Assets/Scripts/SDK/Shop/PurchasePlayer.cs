using System;
using YG.Utils.Pay;

public class PurchasePlayer : IPurchase
{
    public PurchasePlayer(Purchase purchase)
    {
        if (purchase == null) 
            throw new ArgumentNullException(nameof(purchase));

        Id = purchase.id;
        IsConsumed = purchase.consumed;
    }

    public string Id { get; }
    public bool IsConsumed { get; }
}
