using CannonTurret.StepSystem.Steps;
using System;

namespace CannonTurret.StepSystem
{
    public class DynamicNextStep : IDynamicEndStep
    {
        private readonly IStepController StepController;

        private IStep _nextStep;

        public DynamicNextStep(IStepController stepController)
        {
            StepController = stepController ?? throw new ArgumentNullException(nameof(stepController));
        }

        public void SetNextStep(IStep nextStep)
        {
            _nextStep = nextStep ?? throw new ArgumentNullException(nameof(nextStep));
        }

        public void End()
        {
            StepController.EstablishNextStep(_nextStep);
        }
    }
}