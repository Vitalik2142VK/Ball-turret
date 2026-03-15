using System.Collections.Generic;

namespace CannonTurret.Actors
{
    public interface IRemovedActorsCollector
    {
        public void Add(IActor actor);

        public void AddRange(IEnumerable<IActor> actors);
    }
}