using CannonTurret.StepSystem.Steps;
using System;

namespace CannonTurret.StepSystem
{
    public class NextStep : IEndStep
    {
        private readonly IStepController StepController;
        private readonly IStep FollowingStep;

        public NextStep(IStepController stepController, IStep followingStep)
        {
            StepController = stepController ?? throw new ArgumentNullException(nameof(stepController));
            FollowingStep = followingStep ?? throw new ArgumentNullException(nameof(followingStep));
        }

        public void End()
        {
            StepController.EstablishNextStep(FollowingStep);
        }
    }
}