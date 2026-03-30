using CannonTurret.Actors;
using CannonTurret.Actors.Enemies;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class PrepareActorsStep : IStep
    {
        private readonly IActorsPreparator _actorsPreparator;
        private readonly IEnemiesController _enemiesController;
        private readonly IDynamicEndStep _dynamicEndStep;
        private readonly IStep _defaultNextStep;

        public PrepareActorsStep(
            IActorsPreparator actorsPreparator,
            IEnemiesController enemiesController,
            IDynamicEndStep dynamicEndStep,
            IStep defaultNextStep)
        {
            _actorsPreparator = actorsPreparator ?? throw new ArgumentNullException(nameof(actorsPreparator));
            _enemiesController = enemiesController ?? throw new ArgumentNullException(nameof(enemiesController));
            _dynamicEndStep = dynamicEndStep ?? throw new ArgumentNullException(nameof(dynamicEndStep));
            _defaultNextStep = defaultNextStep ?? throw new ArgumentNullException(nameof(defaultNextStep));

            _dynamicEndStep.SetNextStep(_defaultNextStep);
        }

        public void Action()
        {
            _enemiesController.Count();

            if (_enemiesController.AreNoEnemies)
                _dynamicEndStep.SetNextStep(_defaultNextStep);

            _actorsPreparator.Prepare();
            _dynamicEndStep.End();
        }
    }
}