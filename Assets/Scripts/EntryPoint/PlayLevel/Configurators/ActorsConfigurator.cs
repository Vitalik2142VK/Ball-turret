using System;
using System.Collections.Generic;
using UnityEngine;
using Scriptable;

namespace PlayLevel
{
    public class ActorsConfigurator : MonoBehaviour
    {
        [SerializeField] private ActorZone _zoneEnemy;
        [SerializeField] private SpawnPointsRepository _spawnPointsRepository;

        [Header("Factories")]
        [SerializeField] private ViewableBonusFactory _bonusFactory;
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private BorderFactory _borderFactory;

        [Header("Attributes")]
        [SerializeField] private MoveAttributes _startMoveAttributes;
        [SerializeField] private MoveAttributes _defaultMoveAttributes;

        private ActorFactoriesRepository _actorFactoriesRepository;
        private IActorHealthModifier _healthModifier;

        public IActorsControllersAccess ControllersAccess { get; private set; }

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

        public void Configure(IDamagedObject turret, ILevel level, IWinStatus winStatus)
        {
            if (turret == null)
                throw new ArgumentNullException(nameof(turret));

            if (winStatus == null)
                throw new ArgumentNullException(nameof(winStatus));

            _healthModifier = level ?? throw new ArgumentNullException(nameof(level));
            
            IActorSpawner actorSpawner = CreatActorSpawner();
            ActorsMover actorsMover = new ActorsMover();
            ActorsRemover actorsRemover = new ActorsRemover();
            ActorsPreparator actorsPreparator = new ActorsPreparator(actorSpawner, actorsMover, _startMoveAttributes, _defaultMoveAttributes);
            ActorsController actorsController = new ActorsController(actorsPreparator, actorsRemover);
            actorsPreparator.SetLevel(level);

            EnemiesAttacker enemiesAttacker = new EnemiesAttacker(turret);
            EnemiesController enemiesController = new EnemiesController(actorsPreparator, enemiesAttacker);

            _zoneEnemy.Initialize(actorsRemover, enemiesAttacker);

            ControllersAccess = new ActorsControllersAccess(actorsController, enemiesController);
        }

        public void AddActorFactory(IActorFactory actorFactory)
        {
            _actorFactoriesRepository.AddFactory(actorFactory);
        }

        private IActorSpawner CreatActorSpawner()
        {
            _enemyFactory.Initialize(_healthModifier);
            _borderFactory.Initialize(_healthModifier);

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
