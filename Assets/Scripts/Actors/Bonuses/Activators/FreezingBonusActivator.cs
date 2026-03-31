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
            _nextStepPrepareActors = nextStepPrepareActors ?? throw new NullReferenceException(nameof(nextStepPrepareActors));
            _view = view ?? throw new NullReferenceException(nameof(view));
            _freezeStep = freezeStep ?? throw new NullReferenceException(nameof(freezeStep));
        }

        public void Activate()
        {
            _nextStepPrepareActors.SetNextStep(_freezeStep);
            _view.PlayActivation();
        }
    }
}