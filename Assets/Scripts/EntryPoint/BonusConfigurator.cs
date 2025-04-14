using System;
using UnityEngine;

public abstract class BonusConfigurator : MonoBehaviour
{
    [Header("Bonus prefab")]
    [SerializeField] private Bonus _bonusPrefab;

    public Bonus BonusPrefab => _bonusPrefab;

    private void OnValidate()
    {
        if (_bonusPrefab == null)
            throw new NullReferenceException(nameof(_bonusPrefab));
    }

    public abstract void Configure();
}