using CannonTurret.Actors.MoveSystem;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class ActorsMoveStep : IStep, IEndPointStep
    {
        private readonly IActorsMover ActorsMover;

        private IEndStep _endStep;

        public ActorsMoveStep(IActorsMover actorsMover)
        {
            ActorsMover = actorsMover ?? throw new ArgumentNullException(nameof(actorsMover));
        }

        public void Action()
        {
            ActorsMover.MoveAll();

            if (ActorsMover.AreMovesFinished)
                _endStep.End();
        }

        public void SetEndStep(IEndStep endStep)
        {
            _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
        }
    }
}