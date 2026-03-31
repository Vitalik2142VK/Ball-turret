using CannonTurret.Actors.MoveSystem;
using System;

namespace CannonTurret.Actors
{
    public class ActorsController : IActorsController
    {
        private readonly IAdvancedActorsPreparator _actorsPreparator;
        private readonly IRemovedActorsRepository _removedActorsRepository;
        private readonly IActorsMover _actorsMover;

        public bool AreMovesFinished => _actorsMover.AreMovesFinished;

        public ActorsController(
            IAdvancedActorsPreparator actorsPreparator,
            IRemovedActorsRepository removedActorsRepository)
        {
            _actorsPreparator = actorsPreparator ?? throw new ArgumentNullException(nameof(actorsPreparator));
            _removedActorsRepository = removedActorsRepository ?? throw new ArgumentNullException(nameof(removedActorsRepository));

            if (_actorsPreparator.ActorsMover == null)
                throw new ArgumentNullException(nameof(_actorsPreparator.ActorsMover));

            _actorsMover = _actorsPreparator.ActorsMover;
        }

        public void MoveAll() => _actorsMover.MoveAll();

        public void Prepare()
        {
            if (_actorsPreparator.EnemiesCount > 0)
            {
                _actorsPreparator.ActivateDebuffablies();
                _actorsPreparator.CountRemainingEnemies();
            }

            if (_actorsPreparator.EnemiesCount == 0)
                RemoveAll();

            _actorsPreparator.Prepare();
        }

        public void RemoveAll()
        {
            var removedActors = _actorsPreparator.PopActors();
            _removedActorsRepository.AddRange(removedActors);
            _removedActorsRepository.RemoveAll();
        }

        public void RemoveAllDisabled()
        {
            _removedActorsRepository.RemoveAll();
            _actorsPreparator.CountRemainingEnemies();
        }
    }
}