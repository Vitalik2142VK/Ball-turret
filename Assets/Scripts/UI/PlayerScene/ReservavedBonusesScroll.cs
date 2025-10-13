using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollAdapter))] 
public class ReservavedBonusesScroll : MonoBehaviour
{
    [SerializeField] private ReservedBonusButton _prefab;
    [SerializeField] private ContentSizeFitter _content;

    private IBonusReservator _bonusReservator;

    private void OnValidate()
    {
        if (_bonusReservator == null)
            throw new NullReferenceException(nameof(_bonusReservator));

        if (_content == null)
            throw new NullReferenceException(nameof(_content));
    }

    public void Initialize(IBonusReservator bonusReservator)
    {
        _bonusReservator = bonusReservator ?? throw new ArgumentNullException(nameof(bonusReservator));

        foreach (var bonusCard in _bonusReservator.GetBonusCards())
        {

        }
    }
}