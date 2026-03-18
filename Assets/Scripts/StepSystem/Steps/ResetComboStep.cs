using CannonTurret.UI.PlayerScene;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class ResetComboStep : IStep, IEndPointStep
    {
        private readonly IComboCounterResetter Resetter;

        private IEndStep _endStep;

        public ResetComboStep(IComboCounterResetter resetter)
        {
            Resetter = resetter ?? throw new ArgumentNullException(nameof(resetter));
        }

        public void Action()
        {
            Resetter.ResetCombo();
            _endStep.End();
        }

        public void SetEndStep(IEndStep endStep)
        {
            _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
        }
    }
}