using System;
using UnityEngine;
using UnityEngine.UI;

public class GameProductWindow : MonoBehaviour
{
    [SerializeField] private GameProductData _data;
    [SerializeField] private AddCoinsButton _addCoinsButton;
    [SerializeField] private Button _updateButton;
    [SerializeField] private Image _maxLevel;

    private IGamePayTransaction _transaction;
    private IImprovementProduct _product;
    private IPurchaseRewardService _rewardService;
    private bool _isReserved;

    public event Action<IGamePayTransaction> Selected;

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
        _maxLevel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _addCoinsButton.Clicked += OnEstablishRewardAd;
        _updateButton.onClick.AddListener(OnSendTransaction);
        _isReserved = false;
    }

    private void OnDisable()
    {
        _addCoinsButton.Clicked -= OnEstablishRewardAd;
        _updateButton.onClick.RemoveListener(OnSendTransaction);
    }

    public void Initialize(IGamePayTransaction transaction, IImprovementProduct product, IPurchaseRewardService rewardService)
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

    public void HandleReservation()
    {
        if (_isReserved == false)
            return;

        _isReserved = false;

        OnSendTransaction();
    }

    private void OnEstablishRewardAd()
    {
        int missingAmount = _transaction.GetMissingAmount();
        _rewardService.AssignReward(missingAmount);
        _isReserved = true;

        ActivateAddCoinsButton(false);
    }

    private void OnSendTransaction()
    {
        Selected?.Invoke(_transaction);
    }

    private void ApplyToTransactionState()
    {
        if (_transaction.IsLocked)
        {
            int missingAmount = _transaction.GetMissingAmount();
            bool canProvideReward = _rewardService.CanProvideReward(_transaction.Price, missingAmount);

            if (canProvideReward)
            {
                _rewardService.AssignReward(missingAmount);
                _addCoinsButton.UpdateData();
            }
            else
            {
                _updateButton.interactable = false;
            }

            ActivateAddCoinsButton(canProvideReward);
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
