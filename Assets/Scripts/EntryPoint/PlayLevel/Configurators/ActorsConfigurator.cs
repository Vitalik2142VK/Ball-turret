using System;
using System.Collections.Generic;
using UnityEngine;
using Scriptable;

namespace PlayLevel
{
    public class ActorsConfigurator : MonoBehaviour
    {
        [SerializeField] private ZoneEnemy _zoneEnemy;
        [SerializeField] private SpawnPointsRepository _spawnPointsRepository;
        [SerializeField, SerializeIterface(typeof(IActorFactory))] private GameObject[] _actorFactories;

        [Header("Attributes")]
        [SerializeField] private MoveAttributes _startMoveAttributes;
        [SerializeField] private MoveAttributes _defaultMoveAttributes;

        public ActorsController ActorsController { get; private set; }

        private void OnValidate()
        {
            if (_zoneEnemy == null)
                throw new NullReferenceException(nameof(_zoneEnemy));

            if (_spawnPointsRepository == null)
                throw new NullReferenceException(nameof(_spawnPointsRepository));

            if (_actorFactories == null || _actorFactories.Length == 0)
                throw new InvalidOperationException(nameof(_actorFactories));

            if (_startMoveAttributes == null)
                throw new NullReferenceException(nameof(_startMoveAttributes));

            if (_defaultMoveAttributes == null)
                throw new NullReferenceException(nameof(_defaultMoveAttributes));
        }

        public void Configure(IDamagedObject turret, ILevelActorsPlanner levelActorsPlanner)
        {
            if (turret == null)
                throw new ArgumentNullException(nameof(turret));

            if (levelActorsPlanner == null)
                throw new ArgumentNullException(nameof(levelActorsPlanner));

            IActorSpawner actorSpawner = CreatActorSpawner();
            IAdvancedActorsMover actorMover = new ActorsMover();
            IRemovedActorsRepository removedActorsRepository = new ActorsRemover();
            EnemiesAttacker enemiesAttacker = new EnemiesAttacker(turret);
            ActorsPreparator actorsPreparator = new ActorsPreparator(actorSpawner, actorMover, _startMoveAttributes, _defaultMoveAttributes);
            actorsPreparator.SetLevelActorsPlanner(levelActorsPlanner);

            _zoneEnemy.Initialize(removedActorsRepository, enemiesAttacker);

            ActorsController = new ActorsController(actorsPreparator, removedActorsRepository, enemiesAttacker);
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
}
