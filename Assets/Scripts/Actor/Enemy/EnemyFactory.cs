using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IActorFactory
{
    [SerializeField, SerializeIterface(typeof(IEnemyCreator))] private GameObject[] _enemyCreators;

    private Dictionary<string, IEnemyCreator> _creators;
    private IActorHealthModifier _healthModifier;

    private void OnValidate()
    {
        if (_enemyCreators == null || _enemyCreators.Length == 0)
            throw new InvalidOperationException(nameof(_enemyCreators));

        foreach (var gameObject in _enemyCreators)
            if (gameObject.TryGetComponent(out IEnemyCreator _) == false)
                throw new InvalidOperationException($"One or more objects do not have a component <{nameof(IEnemyCreator)}>");
    }

    private void Awake()
    {
        _creators = CreateDictionaryPrefabs();
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

    private Dictionary<string, IEnemyCreator> CreateDictionaryPrefabs()
    {
        int lenght = _enemyCreators.Length;
        Dictionary<string, IEnemyCreator> prefabs = new Dictionary<string, IEnemyCreator>(lenght);

        foreach (var gameObject in _enemyCreators)
            if (gameObject.TryGetComponent(out IEnemyCreator creator))
                prefabs.Add(creator.Name, creator);

        return prefabs;
    }
}
