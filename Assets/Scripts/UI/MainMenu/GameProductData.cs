using System;
using TMPro;
using UnityEngine;

public class GameProductData : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentValue;
    [SerializeField] private TextMeshProUGUI _improveValue;
    [SerializeField] private TextMeshProUGUI _price;

    private void OnValidate()
    {
        if (_currentValue == null)
            throw new NullReferenceException(nameof(_currentValue));

        if (_improveValue == null)
            throw new NullReferenceException(nameof(_improveValue));

        if (_price == null)
            throw new NullReferenceException(nameof(_price));
    }

    public void SetActive(bool isActive) => gameObject.SetActive(isActive);

    public void SetCurrentValue(string currentValue)
    {
        if (string.IsNullOrEmpty(currentValue))
            throw new ArgumentException(nameof(currentValue));

        _currentValue.text = currentValue;
    }

    public void SetImproveValue(string improveValue)
    {
        if (string.IsNullOrEmpty(improveValue))
            throw new ArgumentException(nameof(improveValue));

        _improveValue.text = improveValue;
    }

    public void SetPrice(string price)
    {
        if (string.IsNullOrEmpty(price))
            throw new ArgumentException(nameof(price));

        _price.text = price;
    }
}