using System.Collections.Generic;

public interface IRemovedActorsCollector
{
    public void Add(IActor actor);

    public void AddRange(IEnumerable<IActor> actors);
}