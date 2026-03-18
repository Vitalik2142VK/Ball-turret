using CannonTurret.Actors;
using CannonTurret.Actors.Enemies;
using CannonTurret.LevelSystem;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class CyclicalStep : IStep
    {
        private readonly IActorsRemover ActorsRemover;
        private readonly IEnemiesController EnemiesController;
        private readonly ILevelStatus LevelStatus;
        private readonly IDynamicEndStep DynamicEndStep;

        private IStep _startStep;
        private IStep _loopingStep;
        private IStep _finishStep;

        public CyclicalStep(IDynamicEndStep dynamicEndStep, IActorsRemover actorsRemover, IEnemiesController enemiesController, ILevelStatus levelStatus)
        {
            DynamicEndStep = dynamicEndStep ?? throw new ArgumentNullException(nameof(dynamicEndStep));
            ActorsRemover = actorsRemover ?? throw new ArgumentNullException(nameof(actorsRemover));
            EnemiesController = enemiesController ?? throw new ArgumentNullException(nameof(enemiesController));
            LevelStatus = levelStatus ?? throw new ArgumentNullException(nameof(levelStatus));
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
            if (EnemiesController.AreNoEnemies && LevelStatus.IsComplete || LevelStatus.IsLose)
            {
                DynamicEndStep.SetNextStep(_finishStep);
            }
            else if (EnemiesController.AreNoEnemies)
            {
                ActorsRemover.RemoveAll();
                DynamicEndStep.SetNextStep(_startStep);
            }
            else
            {
                DynamicEndStep.SetNextStep(_loopingStep);
            }

            DynamicEndStep.End();
        }
    }
}