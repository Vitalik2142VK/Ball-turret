using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using YG;
using YG.Utils.Pay;

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
        _paymentsCatalog.UpdatePurchasesList();
        var purchases = _paymentsCatalog.purchases;
        _purchases = new Dictionary<string, IPurchase>(purchases.Length);

        Console.GetLog($"Purchases: ");

        foreach (var purchase in purchases)
        {
            var purchaseData = purchase.data;
            _purchases.Add(purchaseData.id, new PurchasePlayer(purchaseData));

            Console.GetLog($"Id = {purchaseData.id}, description = {purchaseData.description}, price = {purchaseData.price}, consumed = {purchaseData.consumed}");
        }

        var disableAdsId = PurchasesTypes.DisableAds;

        if (_purchases[disableAdsId].IsConsumed)
            RemoveDisableAdsButton(disableAdsId);
    }

    public bool HasPurchseId(string id)
    {
        if (id == null || id.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(id));

        return _purchases.ContainsKey(id);
    }

    public IPurchase GetPurchase(string id)
    {
        if (HasPurchseId(id) == false)
            throw new ArgumentOutOfRangeException(nameof(id));

        return _purchases[id];
    }

    public bool TryGetPurchase(out IPurchase purchase, string id)
    {
        purchase = null;

        if (HasPurchseId(id))
        {
            purchase = _purchases[id];

            return true;
        }

        return false;
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
            {
                var purchase = _paymentsCatalog.purchases[i].data;

                Console.GetLog($"DisableAds purchase: Id = {purchase.id}, description = {purchase.description}, price = {purchase.price}, consumed = {purchase.consumed}");

                Destroy(_paymentsCatalog.purchases[i].gameObject);
            }
        }
    }
}
