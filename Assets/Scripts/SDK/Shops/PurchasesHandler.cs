using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

[RequireComponent(typeof(ConsumePurchasesYG))]
public class PurchasesHandler : MonoBehaviour
{
    private Dictionary<string, IPurchaseActivator> _purchaseActivator;

    private void OnEnable()
    {
        YG2.onPurchaseSuccess += OnActivatePurchase;
    }

    private void OnDisable()
    {
        YG2.onPurchaseSuccess -= OnActivatePurchase;
    }

    public void LoadPurchases(IPurchasesStorage purchasesStorage)
    {
        if (purchasesStorage == null)
            throw new ArgumentNullException(nameof(purchasesStorage));

        var purchases = YG2.purchases;
        _purchaseActivator = new Dictionary<string, IPurchaseActivator>(purchases.Length);

        foreach (var purchase in purchases)
        {
            var purchaseId = purchase.id;

            if (purchasesStorage.TryGetPurchase(out IPlayerPurchase playerPurchase, purchaseId))
            {
                IPurchaseActivator activator = new OneTimePurchaseActivator(playerPurchase);
                _purchaseActivator.Add(purchaseId, activator);

                if (purchase.consumed == false)
                    activator.Activate(purchaseId);
            }
        }       
    }

    private void OnActivatePurchase(string purchaseId)
    {
        if (_purchaseActivator.ContainsKey(purchaseId) == false)
            throw new ArgumentOutOfRangeException(nameof(purchaseId));

        _purchaseActivator[purchaseId].Activate(purchaseId);
    }
}
