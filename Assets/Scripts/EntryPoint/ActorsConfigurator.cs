using System;
using System.Collections.Generic;
using UnityEngine;

public class ActorsConfigurator : MonoBehaviour
{
    [SerializeField] private ZoneEnemy _zoneEnemy;
    [SerializeField] private SpawnPointsRepository _spawnPointsRepository;
    [SerializeField, SerializeIterface(typeof(IActorFactory))] private GameObject[] _actorFactories;

    [Header("Attributes")]
    [SerializeField] private MoveAttributes _startMoveAttributes;
    [SerializeField] private MoveAttributes _defaultMoveAttributes;

    [Header("LevelActorsPlanner (Change to data player)")]
    [SerializeField] private LevelActorsPlanner _levelActorsPlanner;

    public ActorsController ActorsController { get; private set; }

    private void OnValidate()
    {
        if (_zoneEnemy == null)
            throw new NullReferenceException(nameof(_zoneEnemy));

        if (_spawnPointsRepository == null)
            throw new NullReferenceException(nameof(_spawnPointsRepository));

        if (_actorFactories == null || _actorFactories.Length == 0)
            throw new InvalidOperationException(nameof(_spawnPointsRepository));

        if (_startMoveAttributes == null)
            throw new NullReferenceException(nameof(_startMoveAttributes));

        if (_defaultMoveAttributes == null)
            throw new NullReferenceException(nameof(_defaultMoveAttributes));
    }

    public void Configure(IDamagedObject turret)
    {
        if (turret == null)
            throw new ArgumentNullException(nameof(turret));

        IActorSpawner actorSpawner = CreatActorSpawner();
        IActorsMover actorMover = new ActorsMover();
        ActorsRemover actorsRemover = new ActorsRemover();
        EnemiesAttacker enemiesAttacker = new EnemiesAttacker(turret);

        _zoneEnemy.Initialize(actorsRemover, enemiesAttacker);

        ActorsControllerBuilder builder = new ActorsControllerBuilder();
        ActorsController = builder
            .LevelActorsPlanner(_levelActorsPlanner)
            .ActorSpawner(actorSpawner)
            .ActorsMover(actorMover)
            .ActorsRemover(actorsRemover)
            .EnemiesAttacker(enemiesAttacker)
            .StartMoveAttributes(_startMoveAttributes)
            .DefaultMoveAttributes(_defaultMoveAttributes)
            .Build();
    }

    private IActorSpawner CreatActorSpawner()
    {
        List<IActorFactory> factories = new List<IActorFactory>(_actorFactories.Length);

        for (int i = 0; i < _actorFactories.Length; i++)
            factories.Add(_actorFactories[i].GetComponent<IActorFactory>());

        IActorFactoriesRepository actorFactoriesRepository = new ActorFactoriesRepository(factories);

        return new ActorSpawner(_spawnPointsRepository, actorFactoriesRepository);
    }
}
