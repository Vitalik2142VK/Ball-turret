using System;
using UnityEngine;

namespace RecorderLevel
{
    public class EnemiesConfigurator : MonoBehaviour
    {
        [SerializeField] private RecordingEnemyPresenter[] _enemies;
        [SerializeField] private Scriptable.MoveAttributes _moveAttributes;

        public IActorsMover ActorsMover { get; private set; }

        private void OnValidate()
        {
            if (_enemies == null || _enemies.Length == 0)
                throw new InvalidOperationException(nameof(_enemies));

            foreach (var enemy in _enemies)
                if (enemy == null)
                    throw new NullReferenceException($"{_enemies} contains null objects");

            if (_moveAttributes == null)
                throw new NullReferenceException(nameof(_moveAttributes));
        }

        public void Configure()
        {
            ActorsMover actorsMover = new ActorsMover();
            actorsMover.SetMoveAttributes(_moveAttributes);
            actorsMover.SetMovableObjects(_enemies);

            ActorsMover = actorsMover;
        }
    }
}
