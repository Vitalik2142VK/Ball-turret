using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PurchasesStorage : MonoBehaviour, IPurchasesStorage
{
    private Dictionary<string, IPurchase> _purchases;

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
        var purchases = YandexGame.purchases;
        _purchases = new Dictionary<string, IPurchase>(purchases.Length);

        foreach (var purchase in purchases)
            _purchases.Add(purchase.id, new PurchasePlayer(purchase));
    }

    public bool IsContainsId(string id)
    {
        if (id == null || id.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(id));

        return _purchases.ContainsKey(id);
    }

    public IPurchase GetPurchase(string id)
    {
        if (_purchases.ContainsKey(id))
            throw new ArgumentOutOfRangeException(nameof(id));

        return _purchases[id];
    }

    private void SuccessPurchased(string id)
    {
        //// Ваш код для обработки покупки. Например:
        //if (id == "50")
        //    YandexGame.savesData.money += 50;
        //else if (id == "250")
        //    YandexGame.savesData.money += 250;
        //else if (id == "1500")
        //    YandexGame.savesData.money += 1500;
        //YandexGame.SaveProgress();
    }
}
