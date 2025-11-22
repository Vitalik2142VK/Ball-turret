using System;
using UnityEngine;
using UnityEngine.UI;

public class GameProductWindow : MonoBehaviour
{
    [SerializeField] private GameProductData _data;
    [SerializeField] private Button _updateButton;
    [SerializeField] private Button _addCoinsButton;
    [SerializeField] private Image _maxLevel;

    private IGamePayTransaction _transaction;
    private IImprovementProduct _product;

    public event Action<IGamePayTransaction> Clicked;

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
        _updateButton.onClick.AddListener(OnSendTransaction);
        _addCoinsButton.onClick.AddListener(OnEstablishRevardAd);
    }

    private void OnDisable()
    {
        _updateButton.onClick.RemoveListener(OnSendTransaction);
        _addCoinsButton.onClick.RemoveListener(OnEstablishRevardAd);
    }

    public void Initialize(IGamePayTransaction transaction, IImprovementProduct product)
    {
        _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        _product = product ?? throw new ArgumentNullException(nameof(product));
    }

    public void UpdateData()
    {
        if (_transaction.IsLocked || _product.CanImprove == false)
            Disable();

        _data.SetCurrentValue(_product.CurrentValue.ToString());
        _data.SetImproveValue(_product.ImproveValue.ToString());
        _data.SetPrice(_transaction.Price.ToString());
    }

    private void OnSendTransaction()
    {
        Clicked?.Invoke(_transaction);
    }

    private void OnEstablishRevardAd()
    {
        throw new NotImplementedException();
    }

    private void Disable()
    {
        if (_product.CanImprove == false)
        {
            _data.SetActive(false);
            _maxLevel.gameObject.SetActive(true);

            return;
        }

        if (_transaction.IsLocked)
        {

        }
    }
}
