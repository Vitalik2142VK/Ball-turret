using CannonTurret.Actors;
using CannonTurret.Actors.Enemies;
using CannonTurret.LevelSystem;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class CyclicalStep : IStep
    {
        private readonly IActorsRemover _actorsRemover;
        private readonly IEnemiesController _enemiesController;
        private readonly ILevelStatus _levelStatus;
        private readonly IDynamicEndStep _dynamicEndStep;

        private IStep _startStep;
        private IStep _loopingStep;
        private IStep _finishStep;

        public CyclicalStep(
            IDynamicEndStep dynamicEndStep,
            IActorsRemover actorsRemover,
            IEnemiesController enemiesController,
            ILevelStatus levelStatus)
        {
            _dynamicEndStep = dynamicEndStep ?? throw new ArgumentNullException(nameof(dynamicEndStep));
            _actorsRemover = actorsRemover ?? throw new ArgumentNullException(nameof(actorsRemover));
            _enemiesController = enemiesController ?? throw new ArgumentNullException(nameof(enemiesController));
            _levelStatus = levelStatus ?? throw new ArgumentNullException(nameof(levelStatus));
        }

        public void SetStartStep(IStep startStep)
        {
            _startStep = startStep ?? throw new ArgumentNullException(nameof(startStep));
        }

        public void SetLoopingStep(IStep loopingStep)
        {
            _loopingStep = loopingStep ?? throw new ArgumentNullException(nameof(loopingStep));
        }

        public void SetFinishStep(IStep finishStep)
        {
            _finishStep = finishStep ?? throw new ArgumentNullException(nameof(finishStep));
        }

        public void Action()
        {
            if (_enemiesController.AreNoEnemies && _levelStatus.IsComplete || _levelStatus.IsLose)
            {
                _dynamicEndStep.SetNextStep(_finishStep);
            }
            else if (_enemiesController.AreNoEnemies)
            {
                _actorsRemover.RemoveAll();
                _dynamicEndStep.SetNextStep(_startStep);
            }
            else
            {
                _dynamicEndStep.SetNextStep(_loopingStep);
            }

            _dynamicEndStep.End();
        }
    }
}