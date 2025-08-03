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

        [Header("Factories")]
        [SerializeField] private BonusFactory _bonusFactory;
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private BorderFactory _borderFactory;

        [Header("Attributes")]
        [SerializeField] private MoveAttributes _startMoveAttributes;
        [SerializeField] private MoveAttributes _defaultMoveAttributes;

        private ActorFactoriesRepository _actorFactoriesRepository;

        public ActorsController ActorsController { get; private set; }

        private void OnValidate()
        {
            if (_zoneEnemy == null)
                throw new NullReferenceException(nameof(_zoneEnemy));

            if (_spawnPointsRepository == null)
                throw new NullReferenceException(nameof(_spawnPointsRepository));

            if (_bonusFactory == null)
                throw new NullReferenceException(nameof(_bonusFactory));

            if (_enemyFactory == null)
                throw new NullReferenceException(nameof(_enemyFactory));

            if (_borderFactory == null)
                throw new NullReferenceException(nameof(_borderFactory));

            if (_startMoveAttributes == null)
                throw new NullReferenceException(nameof(_startMoveAttributes));

            if (_defaultMoveAttributes == null)
                throw new NullReferenceException(nameof(_defaultMoveAttributes));
        }

        public void Configure(IDamagedObject turret, ILevel level)
        {
            if (turret == null)
                throw new ArgumentNullException(nameof(turret));

            if (level == null)
                throw new ArgumentNullException(nameof(level));
            
            IActorSpawner actorSpawner = CreatActorSpawner();
            IAdvancedActorsMover actorMover = new ActorsMover();
            IRemovedActorsRepository removedActorsRepository = new ActorsRemover();
            EnemiesAttacker enemiesAttacker = new EnemiesAttacker(turret);
            ActorsPreparator actorsPreparator = new ActorsPreparator(actorSpawner, actorMover, _startMoveAttributes, _defaultMoveAttributes);
            actorsPreparator.SetLevelActorsPlanner(level);

            _zoneEnemy.Initialize(removedActorsRepository, enemiesAttacker);

            ActorsController = new ActorsController(actorsPreparator, removedActorsRepository, enemiesAttacker);
        }

        public void AddActorFactory(IActorFactory actorFactory)
        {
            _actorFactoriesRepository.AddFactory(actorFactory);
        }

        private IActorSpawner CreatActorSpawner()
        {
            List<IActorFactory> factories = new List<IActorFactory>
            {
                _enemyFactory,
                _bonusFactory,
                _borderFactory
            };

            _actorFactoriesRepository = new ActorFactoriesRepository(factories);

            return new ActorSpawner(_spawnPointsRepository, _actorFactoriesRepository);
        }
    }
}
