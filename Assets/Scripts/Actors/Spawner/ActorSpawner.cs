using System;
using System.Collections.Generic;
using UnityEngine;

public class ActorSpawner : IActorSpawner
{
    private ISpawnPointsRepository _spawnPointsRepository;
    private IActorFactoriesRepository _factoryRepository;

    public ActorSpawner(ISpawnPointsRepository spawnPointsRepository, IActorFactoriesRepository factoryRepository)
    {
        _spawnPointsRepository = spawnPointsRepository ?? throw new ArgumentNullException(nameof(spawnPointsRepository));
        _factoryRepository = factoryRepository ?? throw new ArgumentNullException(nameof(factoryRepository));
    }

    public List<IActor> Spawn(IWaveActorsPlanner planner)
    {
        if (planner == null)
            throw new ArgumentNullException(nameof(planner));

        IReadOnlyCollection<IActorPlanner> actorPlanners = planner.GetActorPlanners();

        if (actorPlanners.Count == 0)
            throw new InvalidOperationException($"{nameof(actorPlanners)} is empty");

        List<IActor> actors = new List<IActor>(actorPlanners.Count);

        foreach (var actorPlanner in actorPlanners)
        {
            IActor actor = CreatActor(actorPlanner);
            actors.Add(actor);
        }

        return actors;
    }

    private IActor CreatActor(IActorPlanner actorPlanner)
    {
        string nameActor = actorPlanner.NameActor;
        Vector3 startPosition = _spawnPointsRepository.GetPositionSpawnPoint(actorPlanner.ColumnNumber, actorPlanner.LineNumber);
        IActorFactory actorFactory = _factoryRepository.GetFactoryByNameTypeActor(nameActor);
        IActor actor = actorFactory.Create(nameActor);
        actor.SetStartPosition(startPosition);

        return actor;
    }

    //Debug
    private void DebugActorPlanners(IReadOnlyCollection<IActorPlanner> actorPlanners)
    {
        foreach (var actorPlanner in actorPlanners)
        {
            Debug.Log($"NameActor - {actorPlanner.NameActor} | columnNum - {actorPlanner.ColumnNumber} | lineNum - {actorPlanner.LineNumber}");
        }
    }
}
