using System.Collections.Generic;

public class ActorsRemover : IActorsRemover, IRemovedActorsCollector
{
    private List<IActor> _removedActors;

    public ActorsRemover()
    {
        _removedActors = new List<IActor>();
    }

    public void Add(IActor actor)
    {
        if (actor == null)
            throw new System.ArgumentNullException(nameof(actor));

        _removedActors.Add(actor);
    }

    public void RemoveAll()
    {
        foreach (var actor in _removedActors)
            actor.Destroy();

        _removedActors.Clear();
    }
}
