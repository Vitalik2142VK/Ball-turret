using System;
using System.Linq;
using UnityEngine;

public class BonusesPrefabConfigurator : MonoBehaviour
{
    [SerializeField] private BonusConfigurator[] _bonusConfigurators;

    private void OnValidate()
    {
        if (_bonusConfigurators == null)
            throw new NullReferenceException(nameof(_bonusConfigurators));

        if (_bonusConfigurators.Length == 0)
            throw new InvalidOperationException(nameof(_bonusConfigurators));
    }

    public void Configure()
    {
        foreach (var bonusConfigurator in _bonusConfigurators)
            bonusConfigurator.Configure();
    }

    public Bonus[] GetBonusPrefabs()
    {
        return _bonusConfigurators.Select(bonusConfigurator => bonusConfigurator.BonusPrefab).ToArray();
    }
}
