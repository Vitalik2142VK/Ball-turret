using System;
using UnityEngine;

public abstract class BonusConfigurator : MonoBehaviour
{
    [Header("Bonus prefab")]
    [SerializeField] private Bonus _bonusPrefab;
    [SerializeField] private bool _isItRandom;

    public Bonus BonusPrefab => _bonusPrefab;
    public bool IsItRandom => _isItRandom;

    private void OnValidate()
    {
        if (_bonusPrefab == null)
            throw new NullReferenceException(nameof(_bonusPrefab));
    }

    public abstract void Configure();
}
