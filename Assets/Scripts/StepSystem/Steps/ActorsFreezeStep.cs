using CannonTurret.Effects.Freezing;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class ActorsFreezeStep : IStep, IEndPointStep
    {
        private readonly IDynamicEndStep NextStepPrepareActors;
        private readonly IActorsFreezerView Freezer;
        private readonly IStep InterruptedStep;

        private IEndStep _endStep;

        public ActorsFreezeStep(IDynamicEndStep nextStepPrepareActors, IStep interruptedStep, IActorsFreezerView freezer)
        {
            NextStepPrepareActors = nextStepPrepareActors ?? throw new NullReferenceException(nameof(nextStepPrepareActors));
            InterruptedStep = interruptedStep ?? throw new NullReferenceException(nameof(interruptedStep));
            Freezer = freezer ?? throw new NullReferenceException(nameof(freezer));
        }

        public void Action()
        {
            NextStepPrepareActors.SetNextStep(InterruptedStep);
            Freezer.Defrost();
            _endStep.End();
        }

        public void SetEndStep(IEndStep endStep)
        {
            _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
        }
    }
}