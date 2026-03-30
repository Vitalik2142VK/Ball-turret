using CannonTurret.UI.PlayerScene;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class ResetComboStep : IStep, IEndPointStep
    {
        private readonly IComboCounterResetter _resetter;

        private IEndStep _endStep;

        public ResetComboStep(IComboCounterResetter resetter)
        {
            _resetter = resetter ?? throw new ArgumentNullException(nameof(resetter));
        }

        public void Action()
        {
            _resetter.ResetCombo();
            _endStep.End();
        }

        public void SetEndStep(IEndStep endStep)
        {
            _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
        }
    }
}