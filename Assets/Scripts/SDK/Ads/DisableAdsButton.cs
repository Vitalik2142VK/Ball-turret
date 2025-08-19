using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Button))]
public class DisableAdsButton : MonoBehaviour
{
    private const string DisableAdsPurchseId = PurchasesTypes.DisableAds;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnPayPurchase);
        YG2.onPurchaseSuccess += OnRemove;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnPayPurchase);
        YG2.onPurchaseSuccess -= OnRemove;
    }

    public void Initialize(IPurchasesStorage purchasesStorage)
    {
        if (purchasesStorage == null)
            throw new ArgumentNullException(nameof(purchasesStorage));

        if (purchasesStorage.TryGetPurchase(out IPlayerPurchase playerPurchase, DisableAdsPurchseId) == false)
            throw new ArgumentOutOfRangeException($"Purchase with id '{DisableAdsPurchseId}' not found.");

        if (playerPurchase.IsPurchased)
            gameObject.SetActive(false);
    }

    private void OnPayPurchase()
    {
        YG2.BuyPayments(DisableAdsPurchseId);
    }

    private void OnRemove(string purchseId)
    {
        if (purchseId != DisableAdsPurchseId)
            return;

        gameObject.SetActive(false);
    }
}
