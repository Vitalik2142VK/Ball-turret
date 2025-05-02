using System;
using System.Collections.Generic;
using UnityEngine;

public class BonusFactory : MonoBehaviour, IActorFactory
{
    private Dictionary<string, Bonus> _prefabs;

    public void Initialize(Bonus[] bonusPrefabs)
    {
        if (bonusPrefabs == null || bonusPrefabs.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(bonusPrefabs));

        _prefabs = CreateDictionaryPrefabs(bonusPrefabs);
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
        IBonusActivator bonusActivator = prefab.GetCloneBonusActivator();
        Bonus bonus = Instantiate(prefab, prefab.transform.position, transform.rotation);
        bonus.Initialize(bonusActivator);

        return bonus;
    }

    private Dictionary<string, Bonus> CreateDictionaryPrefabs(Bonus[] bonusPrefabs)
    {
        int lenght = bonusPrefabs.Length;
        Dictionary<string, Bonus> prefabs = new Dictionary<string, Bonus>(lenght);

        foreach (var prefab in bonusPrefabs)
            prefabs.Add(prefab.Name, prefab);

        return prefabs;
    }
}
