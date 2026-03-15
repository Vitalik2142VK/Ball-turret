using System;
using TMPro;
using UnityEngine;

public class ReservedBonusView : MonoBehaviour, IReservedBonusView
{
    [SerializeField] private TextMeshProUGUI _numberUses;

    private IReservatedBonusData _reservatedBonusData;

    public bool IsCanActivate => _reservatedBonusData.IsCanActivate;

    private void OnValidate()
    {
        if (_numberUses == null)
            throw new NullReferenceException(nameof(_numberUses));
    }

    public void Initialize(IReservatedBonusData reservatedBonusData)
    {
        _reservatedBonusData ??= reservatedBonusData ?? throw new ArgumentOutOfRangeException(nameof(reservatedBonusData));
    }

    public void UpdateData()
    {
        _numberUses.text = $"{_reservatedBonusData.CurrentBonusesCount}/{_reservatedBonusData.MaxBonusesCount}";
    }
}