using System;
using System.Collections.Generic;
using System.Linq;

namespace CannonTurret.Actors.Spawn
{
    public class ActorFactoriesRepository : IActorFactoriesRepository
    {
        private readonly List<IActorFactory> Factories;

        public ActorFactoriesRepository(IEnumerable<IActorFactory> factories)
        {
            if (factories == null || factories.Count() == 0)
                throw new ArgumentOutOfRangeException(nameof(factories));

            Factories = new List<IActorFactory>(factories);
        }

        public void AddFactory(IActorFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            Factories.Add(factory);
        }

        public IActorFactory GetFactoryByNameTypeActor(string nameTypeActor)
        {
            if (nameTypeActor == null || nameTypeActor.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(nameTypeActor));

            foreach (var factory in Factories)
            {
                if (factory.CanCreate(nameTypeActor))
                    return factory;
            }

            throw new InvalidOperationException($"No factory can create an actor {nameTypeActor}");
        }
    }
}