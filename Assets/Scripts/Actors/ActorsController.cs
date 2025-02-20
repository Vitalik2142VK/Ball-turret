using System;
using System.Collections.Generic;
using UnityEngine;

public class ActorsController : MonoBehaviour, IActorsController
{
    [Header("IMovableObject (Remove)")]                 // change to the spawner
    [SerializeField] private AddBulletBonus[] _bonus;
    [SerializeField] private Enemy[] _enemies;

    private List<IActor> _actors;

    public IActorsMover ActorsMover { get; private set; }
    public IActorsRemover ActorsRemover { get; private set; }
    public IEnemiesAttacker EnemyAttacker { get; private set; }

    private void Awake()
    {
        _actors = new List<IActor>();
        _actors.AddRange(_bonus);
        _actors.AddRange(_enemies);
    }

    public void Initialize(IActorsMover actorsMover, IActorsRemover actorsRemover, IEnemiesAttacker enemiesAttacker)
    {
        ActorsMover = actorsMover ?? throw new ArgumentNullException(nameof(actorsMover));
        ActorsRemover = actorsRemover ?? throw new ArgumentNullException(nameof(actorsRemover));
        EnemyAttacker = enemiesAttacker ?? throw new ArgumentNullException(nameof(enemiesAttacker));
    }

    public void PrepareActors()
    {
        RemoveDisabledActors();

        if (_actors.Count > 0)
            ActorsMover.SetMovableObject(_actors);
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
}
