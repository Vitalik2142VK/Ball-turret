using CannonTurret.Coin.Wallets;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CannonTurret.SDK.Ads
{
    [RequireComponent(typeof(Button), typeof(AdsViewButton))]
    public class AddCoinsButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _addCoinsText;

        private ICoinAdder _coinAdder;
        private IAdsViewer _adsViewer;
        private AdsViewButton _adsViewButton;
        private Button _button;

        public event Action Clicked;

        public bool IsEnbale => _adsViewer.CanShowRewardAd;

        private void OnValidate()
        {
            if (_addCoinsText == null)
                throw new ArgumentNullException(nameof(_addCoinsText));
        }

        private void Awake()
        {
            _adsViewButton = GetComponent<AdsViewButton>();
            _button = GetComponent<Button>();
            _button.interactable = false;
        }

        private void OnEnable()
        {
            if (_coinAdder == null || _adsViewer == null)
                return;

            _adsViewer.TimerRewardAdReseted += OnUpdateInteractable;
            _button.onClick.AddListener(OnClick);
            OnUpdateInteractable();
        }

        private void Start()
        {
            if (_coinAdder == null || _adsViewer == null)
                return;

            _button.interactable = _adsViewer.CanShowRewardAd;
            _addCoinsText.text = $"+{_coinAdder.CoinsCountAdsView}";
        }

        private void OnDisable()
        {
            if (_coinAdder == null || _adsViewer == null)
                return;

            _adsViewer.TimerRewardAdReseted -= OnUpdateInteractable;
            _button.onClick.RemoveListener(OnClick);
        }

        public void Initialize(ICoinAdder coinAdder, IAdsViewer adsViewer, string rewardId)
        {
            _coinAdder = coinAdder ?? throw new ArgumentNullException(nameof(coinAdder));
            _adsViewer = adsViewer ?? throw new ArgumentNullException(nameof(adsViewer));

            if (_adsViewButton == null)
                _adsViewButton = GetComponent<AdsViewButton>();

            _adsViewButton.Initialize(adsViewer, rewardId);
        }

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void UpdateData()
        {
            _addCoinsText.text = $"+{_coinAdder.CoinsCountAdsView}";
        }

        private void OnClick()
        {
            _button.interactable = false;

            Clicked?.Invoke();
        }

        private void OnUpdateInteractable()
        {
            _button.interactable = _adsViewer.CanShowRewardAd;
        }
    }
}