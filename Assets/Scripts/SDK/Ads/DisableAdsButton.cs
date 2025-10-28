using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.Pay;

[RequireComponent(typeof(Button))]
public class DisableAdsButton : MonoBehaviour
{
    private const string DisableAdsPurchseId = PurchasesTypes.DisableAds;

    [SerializeField] private ImageLoadYG _currencyImage;
    [SerializeField] private TextMeshProUGUI _currencyPrice;

    private Button _button;

    private void OnValidate()
    {
        if (_currencyImage == null)
            throw new NullReferenceException(nameof(_currencyImage));

        if (_currencyPrice == null)
            throw new NullReferenceException(nameof(_currencyPrice));
    }

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

        if (playerPurchase.IsPurchased == false)
            Enable(playerPurchase);
        else
            Destroy(gameObject);
    }

    private void Enable(IPlayerPurchase playerPurchase)
    {
        Purchase purchase = YG2.PurchaseByID(playerPurchase.Id);

        if (purchase == null)
            throw new NullReferenceException(nameof(purchase));

        _currencyPrice.text = purchase.priceValue;

#if !UNITY_EDITOR
        var currencyImageURL = purchase.currencyImageURL;

        if (string.IsNullOrEmpty(currencyImageURL))
            _currencyImage.Load(currencyImageURL);

        Console.GetLog($"CurrencyImageURL == '{currencyImageURL}'")
#endif
    }

    private void OnPayPurchase()
    {
        YG2.BuyPayments(DisableAdsPurchseId);
    }

    private void OnRemove(string purchseId)
    {
        if (purchseId != DisableAdsPurchseId)
            return;

        Destroy(gameObject);
    }
}
