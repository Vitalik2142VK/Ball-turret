using CannonTurret.StepSystem.Steps;
using System;

namespace CannonTurret.StepSystem
{
    public class NextStep : IEndStep
    {
        private readonly IStepController _stepController;
        private readonly IStep _nextStep;

        public NextStep(IStepController stepController, IStep nextStep)
        {
            _stepController = stepController ?? throw new ArgumentNullException(nameof(stepController));
            _nextStep = nextStep ?? throw new ArgumentNullException(nameof(nextStep));
        }

        public void End()
        {
            _stepController.EstablishNextStep(_nextStep);
        }
    }
}