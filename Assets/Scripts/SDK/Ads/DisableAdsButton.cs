using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Button))]
public class DisableAdsButton : MonoBehaviour
{
    [SerializeField] private PurchaseYG _disableAdsPurchase;

    private Button _button;

    private void OnValidate()
    {
        if (_disableAdsPurchase == null)
            throw new System.NullReferenceException(nameof(_disableAdsPurchase));
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
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
