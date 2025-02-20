using System;
using UnityEngine;

public class ZoneEnemy : MonoBehaviour
{
    private IAttackingEnemiesCollector _attackingEnemies;
    private IRemovedActorsCollector _removedActors;

    public event Action TargetPointExited;

    private void OnTriggerExit(Collider other)
    {
        CheckExitTargetPoint(other);
        CheckExitActor(other);
    }

    public void Initialize(IRemovedActorsCollector removedActorsCollector, IAttackingEnemiesCollector attackingEnemiesCollector)
    {
        _attackingEnemies = attackingEnemiesCollector ?? throw new ArgumentNullException(nameof(attackingEnemiesCollector));
        _removedActors = removedActorsCollector ?? throw new ArgumentNullException(nameof(removedActorsCollector));
    }

    private void CheckExitTargetPoint(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ITargetPoint _))
            TargetPointExited?.Invoke();
    }

    private void CheckExitActor(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IActor actor) == false)
            return;

        if (actor is IEnemy enemy)
            _attackingEnemies.Add(enemy);

        _removedActors.Add(actor);
    }
}
