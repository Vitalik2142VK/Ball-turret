using System;
using System.Collections.Generic;

public class ActorsPreparator : IAdvancedActorPreparator
{
    private List<IActor> _actors;
    private int _currentWaveNumber;

    private IActorSpawner _spawner;
    private ILevelActorsPlanner _levelActorsPlanner;
    private IAdvancedActorsMover _actorsMover;
    private IMoveAttributes _startMoveAttributes;
    private IMoveAttributes _defaultMoveAttributes;

    public IActorsMover ActorsMover => _actorsMover;
    public bool AreWavesOver => _levelActorsPlanner.CountWaves <= _currentWaveNumber;

    public int EnemiesCount { get; private set; }

    public ActorsPreparator(IActorSpawner spawner, IAdvancedActorsMover actorsMover, IMoveAttributes startMoveAttributes, IMoveAttributes defaultMoveAttributes)
    {
        _spawner = spawner ?? throw new ArgumentNullException(nameof(startMoveAttributes));
        _actorsMover = actorsMover ?? throw new ArgumentNullException(nameof(actorsMover));
        _startMoveAttributes = startMoveAttributes ?? throw new ArgumentNullException(nameof(startMoveAttributes));
        _defaultMoveAttributes = defaultMoveAttributes ?? throw new ArgumentNullException(nameof(defaultMoveAttributes));

        _actors = new List<IActor>();
        _currentWaveNumber = 0;

        EnemiesCount = 0;
    }

    public void SetLevelActorsPlanner(ILevelActorsPlanner levelActorsPlanner)
    {
        _levelActorsPlanner = levelActorsPlanner ?? throw new ArgumentNullException(nameof(levelActorsPlanner));
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

    public List<IActor> PopActors()
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
        {
            if (actor is IDebuffable debuffable && actor.IsEnable == true)
                debuffable.ActivateDebuffs();
        }
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
        {
            if (actor is IEnemy)
                EnemiesCount++;
        }
    }

    private void SpawnActors()
    {
        if (AreWavesOver)
            return;

        IWaveActorsPlanner waveActorsPlanner = _levelActorsPlanner.GetWaveActorsPlanner(++_currentWaveNumber);
        _actors = _spawner.Spawn(waveActorsPlanner);
        _actorsMover.SetMoveAttributes(_startMoveAttributes);
    }
}