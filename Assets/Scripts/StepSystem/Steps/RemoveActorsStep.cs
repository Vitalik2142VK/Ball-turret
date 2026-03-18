using CannonTurret.Actors;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class RemoveActorsStep : IStep, IEndPointStep
    {
        private readonly IDisableActorsRemover ActorsRemover;

        private IEndStep _endStep;

        public RemoveActorsStep(IDisableActorsRemover actorsRemover)
        {
            ActorsRemover = actorsRemover ?? throw new ArgumentNullException(nameof(actorsRemover));
        }

        public void Action()
        {
            ActorsRemover.RemoveAllDisabled();
            _endStep.End();
        }

        public void SetEndStep(IEndStep endStep)
        {
            _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
        }
    }
}