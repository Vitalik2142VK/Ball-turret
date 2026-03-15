using System;
using System.Collections.Generic;

public class BonusFactory
{
    private Dictionary<string, IBonusCreator> _creators;

    public BonusFactory(IEnumerable<IBonusCreator> bonusCreators)
    {
        if (bonusCreators == null)
            throw new ArgumentNullException(nameof(bonusCreators));

        _creators = CreateDictionaryPrefabs(bonusCreators);
    }

    public IBonus Create(string nameTypeActor)
    {
        if (IsCanCreate(nameTypeActor) == false)
            throw new ArgumentOutOfRangeException(nameof(nameTypeActor));

        return _creators[nameTypeActor].Create();
    }

    private bool IsCanCreate(string nameTypeActor)
    {
        if (nameTypeActor == null || nameTypeActor.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(nameTypeActor));

        return _creators.ContainsKey(nameTypeActor);
    }

    private Dictionary<string, IBonusCreator> CreateDictionaryPrefabs(IEnumerable<IBonusCreator> bonusPrefabs)
    {
        Dictionary<string, IBonusCreator> creators = new Dictionary<string, IBonusCreator>();

        foreach (var creator in bonusPrefabs)
            creators.Add(creator.Name, creator);

        if (creators.Count == 0)
            throw new InvalidOperationException($"{nameof(bonusPrefabs)} should not be empty");

        return creators;
    }
}