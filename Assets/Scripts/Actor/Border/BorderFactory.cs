using System;
using System.Collections.Generic;
using UnityEngine;

public class BorderFactory : MonoBehaviour, IActorFactory
{
    [SerializeField] private BorderCreator[] _borderCreators;

    private IActorHealthModifier _healthModifier;
    private Dictionary<string, BorderCreator> _creators;

    private void OnValidate()
    {
        if (_borderCreators == null || _borderCreators.Length == 0)
            throw new InvalidOperationException(nameof(_borderCreators));
    }

    private void Awake()
    {
        _creators = CreateDictionaryCreator();
    }

    public void Initialize(IActorHealthModifier healthModifier)
    {
        _healthModifier = healthModifier ?? throw new ArgumentNullException(nameof(healthModifier));
    }

    public bool IsCanCreate(string nameTypeActor)
    {
        if (nameTypeActor == null || nameTypeActor.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(nameTypeActor));

        return _creators.ContainsKey(nameTypeActor);
    }

    public IActor Create(string nameTypeActor)
    {
        return _creators[nameTypeActor].Create(_healthModifier);
    }

    private Dictionary<string, BorderCreator> CreateDictionaryCreator()
    {
        int lenght = _borderCreators.Length;
        Dictionary<string, BorderCreator> creators = new Dictionary<string, BorderCreator>(lenght);

        foreach (var creator in _borderCreators)
            creators.Add(creator.Name, creator);

        return creators;
    }
}