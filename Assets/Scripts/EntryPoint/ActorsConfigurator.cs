using System;
using UnityEngine;

public class ActorsConfigurator : MonoBehaviour
{
    [SerializeField] private ZoneEnemy _zoneEnemy;
    [SerializeField] private ActorsMover _actorsMover;
    [SerializeField] private ActorsController _actorsController;

    public ActorsController ActorsController => _actorsController;

    private void OnValidate()
    {
        if (_zoneEnemy == null)
            throw new NullReferenceException(nameof(_zoneEnemy));

        if (_actorsMover == null)
            throw new NullReferenceException(nameof(_actorsMover));

        if (_actorsController == null)
            throw new NullReferenceException(nameof(_actorsController));
    }

    public void Configure(IDamagedObject turret)
    {
        if (turret == null)
            throw new ArgumentNullException(nameof(turret));

        ActorsRemover actorsRemover = new ActorsRemover();
        EnemiesAttacker enemiesAttacker = new EnemiesAttacker(turret);

        _zoneEnemy.Initialize(actorsRemover, enemiesAttacker);
        _actorsController.Initialize(_actorsMover, actorsRemover, enemiesAttacker);
    }
}
