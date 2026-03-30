using CannonTurret.Actors;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class RemoveActorsStep : IStep, IEndPointStep
    {
        private readonly IDisableActorsRemover _actorsRemover;

        private IEndStep _endStep;

        public RemoveActorsStep(IDisableActorsRemover actorsRemover)
        {
            _actorsRemover = actorsRemover ?? throw new ArgumentNullException(nameof(actorsRemover));
        }

        public void Action()
        {
            _actorsRemover.RemoveAllDisabled();
            _endStep.End();
        }

        public void SetEndStep(IEndStep endStep)
        {
            _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
        }
    }
}