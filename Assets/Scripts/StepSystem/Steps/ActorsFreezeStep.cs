using CannonTurret.Effects.Freezing;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class ActorsFreezeStep : IStep, IEndPointStep
    {
        private readonly IDynamicEndStep _nextStepPrepareActors;
        private readonly IActorsFreezerView _freezer;
        private readonly IStep _interruptedStep;

        private IEndStep _endStep;

        public ActorsFreezeStep(
            IDynamicEndStep nextStepPrepareActors,
            IStep interruptedStep,
            IActorsFreezerView freezer)
        {
            if (nextStepPrepareActors == null)
                throw new NullReferenceException(nameof(nextStepPrepareActors));

            if (interruptedStep == null)
                throw new NullReferenceException(nameof(interruptedStep));

            if (freezer == null)
                throw new NullReferenceException(nameof(freezer));

            _nextStepPrepareActors = nextStepPrepareActors;
            _interruptedStep = interruptedStep;
            _freezer = freezer;
        }

        public void Action()
        {
            _nextStepPrepareActors.SetNextStep(_interruptedStep);
            _freezer.Defrost();
            _endStep.End();
        }

        public void SetEndStep(IEndStep endStep)
        {
            _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
        }
    }
}