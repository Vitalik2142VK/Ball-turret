using System;
using System.Collections.Generic;
using System.Linq;

public class ActorFactoriesRepository : IActorFactoriesRepository
{
    private List<IActorFactory> _factories;

    public ActorFactoriesRepository(IEnumerable<IActorFactory> factories)
    {
        if (factories == null || factories.Count() == 0)
            throw new ArgumentOutOfRangeException(nameof(factories));

        _factories = new List<IActorFactory>(factories);
    }

    public IActorFactory GetFactoryByNameTypeActor(string nameTypeActor)
    {
        if (nameTypeActor == null || nameTypeActor.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(nameTypeActor));

        foreach (var factory in _factories)
        {
            if (factory.IsCanCreate(nameTypeActor))
                return factory;
        }

        throw new InvalidOperationException($"No factory can create an actor {nameTypeActor}");
    }
}
