using CannonTurret.Actors.Enemies;
using System;
using UnityEngine;

namespace CannonTurret.Actors
{
    [RequireComponent(typeof(BoxCollider))]
    public class ActorZone : MonoBehaviour
    {
        private IAttackingEnemiesCollector _attackingEnemies;
        private IRemovedActorsCollector _removedActors;

        private void OnTriggerExit(Collider other)
        {
            CheckExitActor(other);
        }

        public void Initialize(
            IRemovedActorsCollector removedActorsCollector,
            IAttackingEnemiesCollector attackingEnemiesCollector)
        {
            if (attackingEnemiesCollector == null)
                throw new ArgumentNullException(nameof(attackingEnemiesCollector));

            if (removedActorsCollector == null)
                throw new ArgumentNullException(nameof(removedActorsCollector));

            _attackingEnemies = attackingEnemiesCollector;
            _removedActors = removedActorsCollector;
        }

        private void CheckExitActor(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IActorView actorView) == false)
                return;

            if (actorView is IEnemyView enemyView)
                enemyView.PrepareAttacked(_attackingEnemies);

            actorView.PrepareDeleted(_removedActors);
        }
    }
}