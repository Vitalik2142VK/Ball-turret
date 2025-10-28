using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ChoiceButton), typeof(ScaleButtonAnimator))]
public class ImprovementChoiseButton : MonoBehaviour, IChoiceButton
{
    [SerializeField] private TextMeshProUGUI _currentValue;
    [SerializeField] private TextMeshProUGUI _improveValue;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private Image _infoField;
    [SerializeField] private Image _maxLevel;

    private IGamePayTransaction _transaction;
    private IImprovementProduct _product;
    private IChoiceButton _choiceButton;
    private IButtonAnimator _animator;
    private int _index;

    public event Action<IGamePayTransaction, int> Clicked;

    private void OnValidate()
    {
        if (_currentValue == null)
            throw new NullReferenceException(nameof(_currentValue));

        if (_improveValue == null)
            throw new NullReferenceException(nameof(_improveValue));

        if (_price == null)
            throw new NullReferenceException(nameof(_price));

        if (_infoField == null)
            throw new NullReferenceException(nameof(_infoField));

        if (_maxLevel == null)
            throw new NullReferenceException(nameof(_maxLevel));
    }

    private void Awake()
    {
        _choiceButton = GetComponent<ChoiceButton>();
        _animator = GetComponent<IButtonAnimator>();
        _maxLevel.gameObject.SetActive(false);
    }

    public void Initialize(IGamePayTransaction transaction, IImprovementProduct product)
    {
        _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        _product = product ?? throw new ArgumentNullException(nameof(product));
    }

    public void SetIndex(int index)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        _index = index;
    }

    public void OnClick()
    {
        _animator.Press();
        Clicked?.Invoke(_transaction ,_index);
    }

    public void Enable()
    {
        UpdateData();

        _animator.PressOut();

        if (_transaction.IsLocked || _product.CanBuy == false)
            Disable();
        else
            _choiceButton.Enable();
    }

    public void Disable()
    {
        if (_product.CanBuy == false)
        {
            _infoField.gameObject.SetActive(false);
            _maxLevel.gameObject.SetActive(true);
        }

        _choiceButton.Disable();
    }

    private void UpdateData()
    {
        _currentValue.text = _product.CurrentValue.ToString();
        _improveValue.text = _product.ImproveValue.ToString();
        _price.text = _transaction.Price.ToString();
    }
}
