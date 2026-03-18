using CannonTurret.LevelSystem;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class PlayVictoryStep : IStep, IEndPointStep
    {
        private readonly IVictoryController VictoryController;

        private IEndStep _endStep;

        public PlayVictoryStep(IVictoryController victoryController)
        {
            VictoryController = victoryController ?? throw new ArgumentNullException(nameof(victoryController));
        }

        public void Action()
        {
            VictoryController.PlayVictory();
            _endStep.End();
        }

        public void SetEndStep(IEndStep endStep)
        {
            _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
        }
    }
}