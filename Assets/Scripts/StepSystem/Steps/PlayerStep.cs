using CannonTurret.Actors.Enemies;
using CannonTurret.PlayerSystem;
using CannonTurret.UI;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class PlayerStep : IStep, IEndPointStep
    {
        private readonly IPlayerController PlayerController;
        private readonly IEnemiesController EnemiesController;
        private readonly IActivableUI ReservedBonusesWindow;

        private IEndStep _endStep;

        public PlayerStep(IPlayerController playerController, IEnemiesController enemiesController, IActivableUI reservedBonusesWindow)
        {
            PlayerController = playerController ?? throw new ArgumentNullException(nameof(playerController));
            EnemiesController = enemiesController ?? throw new ArgumentNullException(nameof(enemiesController));
            ReservedBonusesWindow = reservedBonusesWindow ?? throw new ArgumentNullException(nameof(reservedBonusesWindow));
        }

        public void Action()
        {
            if (ReservedBonusesWindow.IsActive == false)
                PlayerController.SelectTarget();

            if (EnemiesController.AreNoEnemies)
                _endStep.End();
        }

        public void SetEndStep(IEndStep endStep)
        {
            _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
        }
    }
}