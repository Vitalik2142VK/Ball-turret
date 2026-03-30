using CannonTurret.StepSystem;
using CannonTurret.StepSystem.Steps;
using System;

namespace CannonTurret.Actors.Bonuses.Activators
{
    public class FreezingBonusActivator : IBonusActivator
    {
        private readonly IDynamicEndStep _nextStepPrepareActors;
        private readonly IBonusActicatorView _view;
        private readonly IStep _freezeStep;

        public FreezingBonusActivator(
            IDynamicEndStep nextStepPrepareActors,
            IBonusActicatorView view,
            IStep freezeStep)
        {
            if (nextStepPrepareActors == null)
                throw new NullReferenceException(nameof(nextStepPrepareActors));

            if (view == null)
                throw new NullReferenceException(nameof(view));

            if (freezeStep == null)
                throw new NullReferenceException(nameof(freezeStep));

            _nextStepPrepareActors = nextStepPrepareActors;
            _view = view;
            _freezeStep = freezeStep;

        }

        public void Activate()
        {
            _nextStepPrepareActors.SetNextStep(_freezeStep);
            _view.PlayActivation();
        }
    }
}