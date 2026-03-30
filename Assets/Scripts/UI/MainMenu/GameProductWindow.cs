using CannonTurret.Coin.Products;
using CannonTurret.Coin.Transactions;
using CannonTurret.Coin.Wallets;
using CannonTurret.SDK.Ads;
using CannonTurret.UI.Animations;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace CannonTurret.UI.MainMenu
{
    [RequireComponent(typeof(PulsingScaleAnimation))]
    public class GameProductWindow : MonoBehaviour
    {
        [SerializeField] private GameProductData _data;
        [SerializeField] private AddCoinsButton _addCoinsButton;
        [SerializeField] private Button _updateButton;
        [SerializeField] private Image _maxLevel;

        private IGamePayTransaction _transaction;
        private IImprovementProduct _product;
        private IPurchaseRewardService _rewardService;
        private PulsingScaleAnimation _animation;

        public event Action<IGamePayTransaction> Selected;

        public bool IsReserved { get; private set; }

        private void OnValidate()
        {
            if (_data == null)
                throw new NullReferenceException(nameof(_data));

            if (_updateButton == null)
                throw new NullReferenceException(nameof(_updateButton));

            if (_addCoinsButton == null)
                throw new NullReferenceException(nameof(_addCoinsButton));

            if (_maxLevel == null)
                throw new NullReferenceException(nameof(_maxLevel));
        }

        private void Awake()
        {
            _animation = GetComponent<PulsingScaleAnimation>();

            _maxLevel.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _addCoinsButton.Clicked += OnEstablishRewardAd;
            _updateButton.onClick.AddListener(OnSendTransaction);
            IsReserved = false;
        }

        private void OnDisable()
        {
            _addCoinsButton.Clicked -= OnEstablishRewardAd;
            _updateButton.onClick.RemoveListener(OnSendTransaction);
        }

        public void Initialize(
            IGamePayTransaction transaction,
            IImprovementProduct product,
            IPurchaseRewardService rewardService)
        {
            _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
            _product = product ?? throw new ArgumentNullException(nameof(product));
            _rewardService = rewardService ?? throw new ArgumentNullException(nameof(rewardService));
        }

        public void UpdateData()
        {
            ApplyToTransactionState();
            ApplyToProductState();
            UpdateViewData();
        }

        public void HandleReservation(bool hasAdsViewedEnd)
        {
            if (IsReserved == false)
                return;

            IsReserved = false;

            if (hasAdsViewedEnd)
                OnSendTransaction();
        }

        private void OnEstablishRewardAd()
        {
            int missingAmount = _transaction.GetMissingAmount();
            _rewardService.AssignReward(missingAmount);
            IsReserved = true;

            ActivateAddCoinsButton(false);
        }

        private void OnSendTransaction()
        {
            _animation.Play();

            Selected?.Invoke(_transaction);
        }

        private void ApplyToTransactionState()
        {
            if (_transaction.IsLocked)
            {
                int missingAmount = _transaction.GetMissingAmount();
                bool isViewingAdsAvailable = 
                    _rewardService.CanProvideReward(_transaction.Price, missingAmount) && _addCoinsButton.IsEnbale;

                if (isViewingAdsAvailable)
                {
                    _rewardService.AssignReward(missingAmount);
                    _addCoinsButton.UpdateData();
                }
                else
                {
                    _updateButton.interactable = false;
                }

                ActivateAddCoinsButton(isViewingAdsAvailable);
            }
            else
            {
                ActivateAddCoinsButton(false);
            }
        }

        private void ApplyToProductState()
        {
            if (_product.CanImprove)
                return;

            _data.SetActive(false);
            _maxLevel.gameObject.SetActive(true);
            _updateButton.interactable = false;
        }

        private void UpdateViewData()
        {
            var currentValue = _product.CurrentValue;
            var improveValue = _product.ImproveValue + currentValue;
            var price = _transaction.Price;

            _data.SetCurrentValue(currentValue.ToString());
            _data.SetImproveValue(improveValue.ToString());
            _data.SetPrice(price.ToString());
        }

        private void ActivateAddCoinsButton(bool IsActive)
        {
            _addCoinsButton.SetActive(IsActive);
            _updateButton.gameObject.SetActive(IsActive == false);
        }
    }
}