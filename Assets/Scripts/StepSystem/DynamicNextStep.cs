using CannonTurret.StepSystem.Steps;
using System;

namespace CannonTurret.StepSystem
{
    public class DynamicNextStep : IDynamicEndStep
    {
        private IStepController _stepController;
        private IStep _nextStep;

        public DynamicNextStep(IStepController stepController)
        {
            _stepController = stepController ?? throw new ArgumentNullException(nameof(stepController));
        }

        public void SetNextStep(IStep nextStep)
        {
            _nextStep = nextStep ?? throw new ArgumentNullException(nameof(nextStep));
        }

        public void End()
        {
            _stepController.EstablishNextStep(_nextStep);
        }
    }
}