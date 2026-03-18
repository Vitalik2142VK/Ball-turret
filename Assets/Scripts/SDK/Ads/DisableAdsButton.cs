using CannonTurret.SDK.Shops;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.Pay;

namespace CannonTurret.SDK.Ads
{
    [RequireComponent(typeof(Button), typeof(ImageLoadYG))]
    public class DisableAdsButton : MonoBehaviour
    {
        private const string DisableAdsPurchseId = PurchasesTypes.DisableAds;

        [SerializeField] private TextMeshProUGUI _currencyPrice;

        private Button _button;
        private ImageLoadYG _imageLoader;

        private void OnValidate()
        {
            if (_currencyPrice == null)
                throw new NullReferenceException(nameof(_currencyPrice));
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _imageLoader = GetComponent<ImageLoadYG>();
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
                Destroy(gameObject);
            else
                Enable(playerPurchase);
        }

        private void Enable(IPlayerPurchase playerPurchase)
        {
            Purchase purchase = YG2.PurchaseByID(playerPurchase.Id);

            if (purchase == null)
                throw new NullReferenceException(nameof(purchase));

            _currencyPrice.text = purchase.priceValue;

            string currencyImageURL = purchase.currencyImageURL;

            if (string.IsNullOrEmpty(currencyImageURL) == false)
                _imageLoader.Load(currencyImageURL);
        }

        private void OnPayPurchase()
        {
            YG2.BuyPayments(DisableAdsPurchseId);
        }

        private void OnRemove(string purchseId)
        {
            if (purchseId != DisableAdsPurchseId)
                return;

            YG2.StickyAdActivity(false);

            Destroy(gameObject);
        }
    }
}