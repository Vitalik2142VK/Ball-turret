using System;
using System.Collections.Generic;
using UnityEngine;

namespace CannonTurret.Actors.Spawn
{
    public class ActorSpawner : IActorSpawner
    {
        private readonly ISpawnPointsRepository SpawnPointsRepository;
        private readonly IActorFactoriesRepository FactoryRepository;

        public ActorSpawner(ISpawnPointsRepository spawnPointsRepository, IActorFactoriesRepository factoryRepository)
        {
            SpawnPointsRepository = spawnPointsRepository ?? throw new ArgumentNullException(nameof(spawnPointsRepository));
            FactoryRepository = factoryRepository ?? throw new ArgumentNullException(nameof(factoryRepository));
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
                IActor actor = CreateActor(actorPlanner);
                actors.Add(actor);
            }

            return actors;
        }

        private IActor CreateActor(IActorPlanner actorPlanner)
        {
            string nameActor = actorPlanner.NameActor;
            Vector3 startPosition = SpawnPointsRepository.GetPositionSpawnPoint(actorPlanner.ColumnNumber, actorPlanner.LineNumber);
            IActorFactory actorFactory = FactoryRepository.GetFactoryByNameTypeActor(nameActor);
            IActor actor = actorFactory.Create(nameActor);
            actor.SetStartPosition(startPosition);

            return actor;
        }
    }
}