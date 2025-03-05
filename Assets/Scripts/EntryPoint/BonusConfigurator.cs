using System;
using UnityEngine;

public abstract class BonusConfigurator : MonoBehaviour
{
    [SerializeField] private Bonus _bonusPrefab;

    public Bonus BonusPrefab => _bonusPrefab;

    public abstract void Configure();

    private void OnValidate()
    {
        if (_bonusPrefab == null)
            throw new NullReferenceException(nameof(_bonusPrefab));
    }
}