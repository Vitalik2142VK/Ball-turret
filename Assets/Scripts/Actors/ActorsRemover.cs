using System;
using System.Collections.Generic;

namespace CannonTurret.Actors
{
    public class ActorsRemover : IRemovedActorsRepository
    {
        private List<IActor> _removedActors;

        public ActorsRemover()
        {
            _removedActors = new List<IActor>();
        }

        public void Add(IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            _removedActors.Add(actor);
        }

        public void AddRange(IEnumerable<IActor> actors)
        {
            if (actors == null)
                throw new ArgumentNullException(nameof(actors));

            _removedActors.AddRange(actors);
        }

        public void RemoveAll()
        {
            foreach (var actor in _removedActors)
                actor.Destroy();

            _removedActors.Clear();
        }
    }
}