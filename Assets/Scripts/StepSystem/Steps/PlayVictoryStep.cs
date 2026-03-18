using CannonTurret.LevelSystem;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class PlayVictoryStep : IStep, IEndPointStep
    {
        private IEndStep _endStep;
        private IVictoryController _victoryController;

        public PlayVictoryStep(IVictoryController victoryController)
        {
            _victoryController = victoryController ?? throw new ArgumentNullException(nameof(victoryController));
        }

        public void Action()
        {
            _victoryController.PlayVictory();
            _endStep.End();
        }

        public void SetEndStep(IEndStep endStep)
        {
            _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
        }
    }
}