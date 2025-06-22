using System;
using System.Collections.Generic;
using UnityEngine;

public class BonusFactory : MonoBehaviour, IActorFactory
{
    private Dictionary<string, CollisionBonus> _prefabs;
    private ISound _takenBonusSound;

    public void Initialize(IEnumerable<CollisionBonus> bonusPrefabs, ISound takenBonusSound)
    {
        if (bonusPrefabs == null)
            throw new ArgumentNullException(nameof(bonusPrefabs));

        _prefabs = CreateDictionaryPrefabs(bonusPrefabs);
        _takenBonusSound = takenBonusSound ?? throw new ArgumentNullException(nameof(takenBonusSound)); ;
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

        CollisionBonus prefab = _prefabs[nameTypeActor];
        IBonusActivator bonusActivator = prefab.GetCloneBonusActivator();
        CollisionBonus collisionBonus = Instantiate(prefab, prefab.transform.position, transform.rotation);
        collisionBonus.Initialize(bonusActivator, _takenBonusSound);

        return collisionBonus;
    }

    private Dictionary<string, CollisionBonus> CreateDictionaryPrefabs(IEnumerable<CollisionBonus> bonusPrefabs)
    {
        Dictionary<string, CollisionBonus> prefabs = new Dictionary<string, CollisionBonus>();

        foreach (var prefab in bonusPrefabs)
            prefabs.Add(prefab.Name, prefab);

        if (prefabs.Count == 0)
            throw new InvalidOperationException($"{nameof(bonusPrefabs)} should not be empty");

        return prefabs;
    }
}
