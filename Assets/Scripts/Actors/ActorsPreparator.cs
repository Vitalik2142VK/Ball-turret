using CannonTurret.Actors.Enemies;
using CannonTurret.Actors.MoveSystem;
using CannonTurret.Actors.Spawn;
using CannonTurret.LevelSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CannonTurret.Actors
{
    public class ActorsPreparator : IAdvancedActorsPreparator
    {
        private List<IActor> _actors;

        private IActorSpawner _spawner;
        private ILevel _level;
        private IAdvancedActorsMover _actorsMover;
        private IMoveAttributes _startMoveAttributes;
        private IMoveAttributes _defaultMoveAttributes;

        public IActorsMover ActorsMover => _actorsMover;
        public bool AreWavesOver => _level.AreWavesOver;

        public int EnemiesCount { get; private set; }

        public ActorsPreparator(IActorSpawner spawner, IAdvancedActorsMover actorsMover, IMoveAttributes startMoveAttributes, IMoveAttributes defaultMoveAttributes)
        {
            _spawner = spawner ?? throw new ArgumentNullException(nameof(startMoveAttributes));
            _actorsMover = actorsMover ?? throw new ArgumentNullException(nameof(actorsMover));
            _startMoveAttributes = startMoveAttributes ?? throw new ArgumentNullException(nameof(startMoveAttributes));
            _defaultMoveAttributes = defaultMoveAttributes ?? throw new ArgumentNullException(nameof(defaultMoveAttributes));

            _actors = new List<IActor>();

            EnemiesCount = 0;
        }

        public void Prepare()
        {
            if (_actors.Count == 0)
            {
                SpawnActors();
                CountEnemies();
            }
            else
            {
                _actorsMover.SetMoveAttributes(_defaultMoveAttributes);
            }

            _actorsMover.SetMovableObjects(_actors);
        }

        public IEnumerable<IActor> PopActors()
        {
            List<IActor> actors = new List<IActor>(_actors);
            _actors.Clear();

            return actors;
        }

        public void CountRemainingEnemies()
        {
            RemoveDisabledActors();
            CountEnemies();
        }

        public void ActivateDebuffablies()
        {
            foreach (var actor in _actors)
                if (actor is IDebuffable debuffable && actor.IsEnable == true)
                    debuffable.ActivateDebuffs();
        }

        public void SetLevel(ILevel level)
        {
            _level = level ?? throw new ArgumentNullException(nameof(level));
        }

        public IEnumerable<IEnemy> GetEnemies()
        {
            return _actors.Where(a => a is IEnemy).Select(a => (IEnemy)a).ToArray();
        }

        private void RemoveDisabledActors()
        {
            for (int i = 0; i < _actors.Count; i++)
            {
                if (_actors[i].IsEnable == false)
                {
                    _actors.RemoveAt(i);
                    i--;
                }
            }
        }

        private void CountEnemies()
        {
            EnemiesCount = 0;

            foreach (var actor in _actors)
                if (actor is IEnemy)
                    EnemiesCount++;
        }

        private void SpawnActors()
        {
            if (_level.TryGetNextWaveActorsPlanner(out IWaveActorsPlanner waveActorsPlanner) == false)
                return;

            _actors = _spawner.Spawn(waveActorsPlanner);
            _actorsMover.SetMoveAttributes(_startMoveAttributes);
        }
    }
}