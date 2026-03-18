using CannonTurret.StepSystem;
using CannonTurret.StepSystem.Steps;
using System;

namespace CannonTurret.Actors.Bonuses.Activators
{
    public class FreezingBonusActivator : IBonusActivator
    {
        private readonly IDynamicEndStep NextStepPrepareActors;
        private readonly IBonusActicatorView View;
        private readonly IStep FreezeStep;

        public FreezingBonusActivator(IDynamicEndStep nextStepPrepareActors, IBonusActicatorView view, IStep freezeStep)
        {
            NextStepPrepareActors = nextStepPrepareActors ?? throw new NullReferenceException(nameof(nextStepPrepareActors));
            View = view ?? throw new NullReferenceException(nameof(view));
            FreezeStep = freezeStep ?? throw new NullReferenceException(nameof(freezeStep));

        }

        public void Activate()
        {
            NextStepPrepareActors.SetNextStep(FreezeStep);
            View.PlayActivation();
        }
    }
}