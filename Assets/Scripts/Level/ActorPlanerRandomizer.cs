using System;
using System.Collections.Generic;

public class ActorPlanerRandomizer<T> : IActorPlanerRandomizer where T : IActor
{
    private List<T> _actors;
    private Random _random;

    public ActorPlanerRandomizer(Random random)
    {
        _actors = new List<T>();
        _random = random ?? throw new ArgumentNullException(nameof(random));
    }

    public void AddActor(T actor)
    {
        if (actor == null)
            throw new ArgumentNullException(nameof(actor));
    }

    public bool TryGetActor(out IActor actor)
    {
        actor = null;
        int nullActorIndex = -1;
        int randomIndex = _random.Next(nullActorIndex, _actors.Count);

        if (randomIndex == nullActorIndex)
            return false;

        actor = _actors[randomIndex];

        return true;
    }
}
