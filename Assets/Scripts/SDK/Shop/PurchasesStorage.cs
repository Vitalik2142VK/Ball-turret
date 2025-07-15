using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

[RequireComponent(typeof(PaymentsCatalogYG))]
public class PurchasesStorage : MonoBehaviour, IPurchasesStorage
{
    private Dictionary<string, IPurchase> _purchases;
    private PaymentsCatalogYG _paymentsCatalog;

    private void Awake()
    {
        _paymentsCatalog = GetComponent<PaymentsCatalogYG>();
    }

    private void OnEnable()
    {
        YandexGame.PurchaseSuccessEvent += SuccessPurchased;
    }

    private void OnDisable()
    {
        YandexGame.PurchaseSuccessEvent -= SuccessPurchased;
    }

    public void LoadPurchases()
    {
        var purchases = _paymentsCatalog.purchases;
        _purchases = new Dictionary<string, IPurchase>(purchases.Length);

        foreach (var purchase in purchases)
        {
            var purchaseData = purchase.data;
            _purchases.Add(purchaseData.id, new PurchasePlayer(purchaseData));
        }

        var disableAdsId = PurchasesTypes.DisableAds;

        if (_purchases[disableAdsId].IsConsumed)
            RemoveDisableAdsButton(disableAdsId);
    }

    public bool IsContainsId(string id)
    {
        if (id == null || id.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(id));

        return _purchases.ContainsKey(id);
    }

    public IPurchase GetPurchase(string id)
    {
        if (IsContainsId(id))
            throw new ArgumentOutOfRangeException(nameof(id));

        return _purchases[id];
    }

    private void SuccessPurchased(string id)
    {
        RemoveDisableAdsButton(id);
    }

    private void RemoveDisableAdsButton(string id)
    {
        if (id != PurchasesTypes.DisableAds)
            return;

        for (int i = 0; i < _paymentsCatalog.purchases.Length; i++)
        {
            if (_paymentsCatalog.purchases[i].data.id == id)
                Destroy(_paymentsCatalog.purchases[i].gameObject);
        }
    }
}
