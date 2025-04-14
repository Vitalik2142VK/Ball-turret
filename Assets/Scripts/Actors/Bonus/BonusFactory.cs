using System;
using System.Collections.Generic;
using UnityEngine;

public class BonusFactory : MonoBehaviour, IActorFactory
{
    [SerializeField] private BonusesPrefabConfigurator _bonusPrefabConfigurator;

    private Dictionary<string, Bonus> _prefabs;

    private void Awake()
    {
        if (_bonusPrefabConfigurator == null)
            throw new NullReferenceException(nameof(_bonusPrefabConfigurator));

        _prefabs = CreateDictionaryPrefabs();
    }

    public bool IsCanCreate(string nameTypeActor)
    {
        if (nameTypeActor == null || nameTypeActor.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(nameTypeActor));

        return _prefabs.ContainsKey(nameTypeActor);
    }

    public IActor Create(string nameTypeActor)
    {
        if (nameTypeActor == null)
            throw new ArgumentNullException(nameof(nameTypeActor));

        if (_prefabs.ContainsKey(nameTypeActor) == false)
            throw new ArgumentOutOfRangeException(nameof(nameTypeActor));

        Bonus prefab = _prefabs[nameTypeActor];
        Bonus bonus = Instantiate(prefab, prefab.transform.position, transform.rotation);

        if (prefab.TryGetComponent(out IBonusActivator bonusActivator))
            bonus.SetBonusActivator(bonusActivator);
        else
            throw new InvalidOperationException();

        return bonus;
    }

    private Dictionary<string, Bonus> CreateDictionaryPrefabs()
    {
        Bonus[] bonusPrefabs = _bonusPrefabConfigurator.GetBonusPrefabs();
        int lenght = bonusPrefabs.Length;
        Dictionary<string, Bonus> prefabs = new Dictionary<string, Bonus>(lenght);

        foreach (var prefab in bonusPrefabs)
            prefabs.Add(prefab.Name, prefab);

        return prefabs;
    }
}
