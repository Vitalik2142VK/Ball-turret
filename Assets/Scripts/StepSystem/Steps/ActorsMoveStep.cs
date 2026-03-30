using CannonTurret.Actors.MoveSystem;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class ActorsMoveStep : IStep, IEndPointStep
    {
        private readonly IActorsMover _actorsMover;

        private IEndStep _endStep;

        public ActorsMoveStep(IActorsMover actorsMover)
        {
            _actorsMover = actorsMover ?? throw new ArgumentNullException(nameof(actorsMover));
        }

        public void Action()
        {
            _actorsMover.MoveAll();

            if (_actorsMover.AreMovesFinished)
                _endStep.End();
        }

        public void SetEndStep(IEndStep endStep)
        {
            _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
        }
    }
}