using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IActorFactory
{
    [SerializeField] private Enemy[] _enemyPrefabs;

    private Dictionary<string, Enemy> _prefabs;
    private ISound _deadSound;

    private void Awake()
    {
        if (_enemyPrefabs == null)
            throw new NullReferenceException(nameof(_enemyPrefabs));

        if (_enemyPrefabs.Length == 0)
            throw new InvalidOperationException(nameof(_enemyPrefabs));

        _prefabs = CreateDictionaryPrefabs();
    }

    public void Initialize(ISound deadSound)
    {
        _deadSound = deadSound ?? throw new ArgumentNullException(nameof(deadSound));
    }

    public bool IsCanCreate(string nameTypeActor)
    {
        if (nameTypeActor == null || nameTypeActor.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(nameTypeActor));

        return _prefabs.ContainsKey(nameTypeActor);
    }

    public IActor Create(string nameTypeActor)
    {
        if (_prefabs.ContainsKey(nameTypeActor) == false)
            throw new ArgumentOutOfRangeException(nameof(nameTypeActor));

        var prefab = _prefabs[nameTypeActor];
        var enemy = Instantiate(prefab, Vector3.zero, prefab.transform.rotation);
        enemy.Initialize(_deadSound);

        return enemy;
    }

    private Dictionary<string, Enemy> CreateDictionaryPrefabs()
    {
        int lenght = _enemyPrefabs.Length;
        Dictionary<string, Enemy> prefabs = new Dictionary<string, Enemy>(lenght);

        foreach (var prefab in _enemyPrefabs)
            prefabs.Add(prefab.Name, prefab);

        return prefabs;
    }
}
