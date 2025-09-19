using System;
using System.Collections.Generic;
using UnityEngine;

public class BonusFactory : MonoBehaviour, IActorFactory
{
    private Dictionary<string, IViewableBonusCreator> _creators;

    public void Initialize(IEnumerable<IViewableBonusCreator> bonusCreators)
    {
        if (bonusCreators == null)
            throw new ArgumentNullException(nameof(bonusCreators));

        _creators = CreateDictionaryPrefabs(bonusCreators);
    }

    public bool IsCanCreate(string nameTypeActor)
    {
        if (nameTypeActor == null || nameTypeActor.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(nameTypeActor));

        return _creators.ContainsKey(nameTypeActor);
    }

    public IActor Create(string nameTypeActor)
    {
        if (IsCanCreate(nameTypeActor) == false)
            throw new ArgumentOutOfRangeException(nameof(nameTypeActor));

        var creator = _creators[nameTypeActor];
        IBonus bonus = creator.Create();

        return creator.Create(bonus);
    }

    private Dictionary<string, IViewableBonusCreator> CreateDictionaryPrefabs(IEnumerable<IViewableBonusCreator> bonusPrefabs)
    {
        Dictionary<string, IViewableBonusCreator> prefabs = new Dictionary<string, IViewableBonusCreator>();


        foreach (var creator in bonusPrefabs)
            prefabs.Add(creator.Name, creator);

        if (prefabs.Count == 0)
            throw new InvalidOperationException($"{nameof(bonusPrefabs)} should not be empty");

        return prefabs;
    }
}
