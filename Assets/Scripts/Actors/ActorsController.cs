using CannonTurret.Actors.MoveSystem;
using System;

namespace CannonTurret.Actors
{
    public class ActorsController : IActorsController
    {
        private readonly IAdvancedActorsPreparator ActorsPreparator;
        private readonly IRemovedActorsRepository RemovedActorsRepository;
        private readonly IActorsMover ActorsMover;

        public bool AreMovesFinished => ActorsMover.AreMovesFinished;

        public ActorsController(IAdvancedActorsPreparator actorsPreparator, IRemovedActorsRepository removedActorsRepository)
        {
            ActorsPreparator = actorsPreparator ?? throw new ArgumentNullException(nameof(actorsPreparator));
            RemovedActorsRepository = removedActorsRepository ?? throw new ArgumentNullException(nameof(removedActorsRepository));

            ActorsMover = ActorsPreparator.ActorsMover ?? throw new NullReferenceException(nameof(ActorsPreparator.ActorsMover));
        }

        public void MoveAll() => ActorsMover.MoveAll();

        public void Prepare()
        {
            if (ActorsPreparator.EnemiesCount > 0)
            {
                ActorsPreparator.ActivateDebuffablies();
                ActorsPreparator.CountRemainingEnemies();
            }

            if (ActorsPreparator.EnemiesCount == 0)
                RemoveAll();

            ActorsPreparator.Prepare();
        }

        public void RemoveAll()
        {
            var removedActors = ActorsPreparator.PopActors();
            RemovedActorsRepository.AddRange(removedActors);
            RemovedActorsRepository.RemoveAll();
        }

        public void RemoveAllDisabled()
        {
            RemovedActorsRepository.RemoveAll();
            ActorsPreparator.CountRemainingEnemies();
        }
    }
}