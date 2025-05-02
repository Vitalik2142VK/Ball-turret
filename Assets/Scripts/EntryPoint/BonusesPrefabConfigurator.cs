using System;
using System.Linq;
using UnityEngine;

public class BonusesPrefabConfigurator : MonoBehaviour
{
    [SerializeField] private BonusConfigurator[] _bonusConfigurators;
    [SerializeField] private BonusFactory _bonusFactory;
    [SerializeField] private ChoiceBonusMenu _choiceBonusMenu;

    private void OnValidate()
    {
        if (_bonusConfigurators == null)
            throw new NullReferenceException(nameof(_bonusConfigurators));

        if (_bonusConfigurators.Length == 0)
            throw new InvalidOperationException(nameof(_bonusConfigurators));

        if (_bonusFactory == null)
            throw new NullReferenceException(nameof(_bonusFactory));

        if (_choiceBonusMenu == null)
            throw new NullReferenceException(nameof(_choiceBonusMenu));
    }

    public void Configure()
    {
        foreach (var bonusConfigurator in _bonusConfigurators)
            bonusConfigurator.Configure();

        var bonusPrefabs = _bonusConfigurators.Select(bc => bc.BonusPrefab).ToArray();
        _bonusFactory.Initialize(bonusPrefabs);

        bonusPrefabs = _bonusConfigurators.Where(bc => bc.IsItRandom).Select(bc => bc.BonusPrefab).ToArray();
        BonusRandomizer bonusRandomizer = new BonusRandomizer(bonusPrefabs);
        _choiceBonusMenu.Initialize(bonusRandomizer);
    }
}
