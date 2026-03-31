using CannonTurret.Actors.Enemies;
using CannonTurret.PlayerSystem;
using CannonTurret.UI;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class PlayerStep : IStep, IEndPointStep
    {
        private readonly IPlayerController _playerController;
        private readonly IEnemiesController _enemiesController;
        private readonly IActivableUI _reservedBonusesWindow;

        private IEndStep _endStep;

        public PlayerStep(
            IPlayerController playerController,
            IEnemiesController enemiesController,
            IActivableUI reservedBonusesWindow)
        {
            _playerController = playerController ?? throw new ArgumentNullException(nameof(playerController));
            _enemiesController = enemiesController ?? throw new ArgumentNullException(nameof(enemiesController));
            _reservedBonusesWindow = reservedBonusesWindow ?? throw new ArgumentNullException(nameof(reservedBonusesWindow));
        }

        public void Action()
        {
            if (_reservedBonusesWindow.IsActive == false)
                _playerController.SelectTarget();

            if (_enemiesController.AreNoEnemies)
                _endStep.End();
        }

        public void SetEndStep(IEndStep endStep)
        {
            _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
        }
    }
}