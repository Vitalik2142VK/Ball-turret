using System;
using System.Collections.Generic;

namespace CannonTurret.Actors
{
    public class ActorsRemover : IRemovedActorsRepository
    {
        private readonly List<IActor> RemovedActors;

        public ActorsRemover()
        {
            RemovedActors = new List<IActor>();
        }

        public void Add(IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            RemovedActors.Add(actor);
        }

        public void AddRange(IEnumerable<IActor> actors)
        {
            if (actors == null)
                throw new ArgumentNullException(nameof(actors));

            RemovedActors.AddRange(actors);
        }

        public void RemoveAll()
        {
            foreach (var actor in RemovedActors)
                actor.Destroy();

            RemovedActors.Clear();
        }
    }
}