using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Button), typeof(PurchaseYG))]
public class DisableAdsButton : MonoBehaviour
{
    private PurchaseYG _disableAdsPurchase;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _disableAdsPurchase.GetComponent<PurchaseYG>();
        _disableAdsPurchase.data.id = PurchasesTypes.DisableAds;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnPayPurchase);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnPayPurchase);
    }

    private void OnPayPurchase()
    {
        _disableAdsPurchase.BuyPurchase();
    }
}
