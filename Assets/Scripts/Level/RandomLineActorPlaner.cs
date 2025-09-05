using System;
using System.Collections.Generic;

public class RandomLineActorPlaner : ILineActorPlaner
{
    private Dictionary<Type, IActorPlanerRandomizer> _randomizers;

    public RandomLineActorPlaner(IEnumerable<IActor> actors)
    {
        _randomizers = CreateRandomizers(actors);
    }

    public IEnumerable<IActorPlanner> GetActorPlanners(int lineNumber)
    {
        throw new System.NotImplementedException();
    }

    private Dictionary<Type, IActorPlanerRandomizer> CreateRandomizers(IEnumerable<IActor> actors)
    {
        Random random = new Random();
        Dictionary<Type, IActorPlanerRandomizer> randomizers = new Dictionary<Type, IActorPlanerRandomizer>()
        {
            { typeof(IEnemy), new ActorPlanerRandomizer<IEnemy>(random) },
            { typeof(IBorder), new ActorPlanerRandomizer<IBorder>(random) },
        };

        foreach (var actor in actors)
        {

        }

        return randomizers;
    }
}
