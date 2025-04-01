using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ZoneEnemy : MonoBehaviour
{
    private IAttackingEnemiesCollector _attackingEnemies;
    private IRemovedActorsCollector _removedActors;
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
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
        return _collider.bounds.Contains(point);
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
