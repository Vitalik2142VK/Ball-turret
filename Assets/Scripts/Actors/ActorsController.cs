using System;
using System.Collections.Generic;

public class ActorsController : IActorsController
{
    private List<IActor> _actors;
    private IActorSpawner _spawner;
    private ILevelActorsPlanner _levelActorsPlanner;
    private IMoveAttributes _startMoveAttributes;
    private IMoveAttributes _defaultMoveAttributes;
    private int _currentWaveNumber;

    public IActorsMover ActorsMover { get; private set; }
    public IActorsRemover ActorsRemover { get; private set; }
    public IEnemiesAttacker EnemyAttacker { get; private set; }

    public ActorsController()
    {
        _actors = new List<IActor>();
        _currentWaveNumber = 0;
    }

    public void SetLevelActorsPlanner(ILevelActorsPlanner levelActorsPlanner)
    {
        _levelActorsPlanner ??= levelActorsPlanner ?? throw new ArgumentNullException(nameof(levelActorsPlanner));
    }

    public void SetActorSpawner(IActorSpawner spawner)
    {
        _spawner ??= spawner ?? throw new ArgumentNullException(nameof(spawner));
    }

    public void SetActorsMover(IActorsMover actorsMover, IMoveAttributes startMoveAttributes, IMoveAttributes defaultMoveAttributes)
    {
        ActorsMover ??= actorsMover ?? throw new ArgumentNullException(nameof(actorsMover));
        _startMoveAttributes ??= startMoveAttributes ?? throw new ArgumentNullException(nameof(startMoveAttributes));
        _defaultMoveAttributes ??= defaultMoveAttributes ?? throw new ArgumentNullException(nameof(defaultMoveAttributes));
    }

    public void SetActorsRemover(IActorsRemover actorsRemover)
    {
        ActorsRemover ??= actorsRemover ?? throw new ArgumentNullException(nameof(actorsRemover));
    }

    public void SetEnemiesAttacker(IEnemiesAttacker enemiesAttacker)
    {
        EnemyAttacker ??= enemiesAttacker ?? throw new ArgumentNullException(nameof(enemiesAttacker));
    }

    public void PrepareActors()
    {
        RemoveDisabledActors();

        if (_actors.Count == 0)
        {
            IWaveActorsPlanner waveActorsPlanner = _levelActorsPlanner.GetWaveActorsPlanner(++_currentWaveNumber);
            _actors = _spawner.Spawn(waveActorsPlanner);
            ActorsMover.SetMoveAttributes(_startMoveAttributes);
        }
        else
        {
            ActorsMover.SetMoveAttributes(_defaultMoveAttributes);
        }

        ActorsMover.SetMovableObjects(_actors);
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
