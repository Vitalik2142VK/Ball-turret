using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ActorZone : MonoBehaviour
{
    private IAttackingEnemiesCollector _attackingEnemies;
    private IRemovedActorsCollector _removedActors;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerExit(Collider other)
    {
        CheckExitActor(other);
    }

    public void Initialize(IRemovedActorsCollector removedActorsCollector, IAttackingEnemiesCollector attackingEnemiesCollector)
    {
        _attackingEnemies = attackingEnemiesCollector ?? throw new ArgumentNullException(nameof(attackingEnemiesCollector));
        _removedActors = removedActorsCollector ?? throw new ArgumentNullException(nameof(removedActorsCollector));
    }

    public bool IsPointInside(Vector3 point)
    {
        return _boxCollider.bounds.Contains(point);
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
