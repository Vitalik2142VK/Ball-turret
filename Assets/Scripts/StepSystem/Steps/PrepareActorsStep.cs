using CannonTurret.Actors;
using CannonTurret.Actors.Enemies;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class PrepareActorsStep : IStep
    {
        private readonly IActorsPreparator ActorsPreparator;
        private readonly IEnemiesController EnemiesController;
        private readonly IDynamicEndStep DynamicEndStep;
        private readonly IStep DefaultNextStep;

        public PrepareActorsStep(IActorsPreparator actorsPreparator, IEnemiesController enemiesController, IDynamicEndStep dynamicEndStep, IStep defaultNextStep)
        {
            ActorsPreparator = actorsPreparator ?? throw new ArgumentNullException(nameof(actorsPreparator));
            EnemiesController = enemiesController ?? throw new ArgumentNullException(nameof(enemiesController));
            DynamicEndStep = dynamicEndStep ?? throw new ArgumentNullException(nameof(dynamicEndStep));
            DefaultNextStep = defaultNextStep ?? throw new ArgumentNullException(nameof(defaultNextStep));

            DynamicEndStep.SetNextStep(DefaultNextStep);
        }

        public void Action()
        {
            EnemiesController.Count();

            if (EnemiesController.AreNoEnemies)
                DynamicEndStep.SetNextStep(DefaultNextStep);

            ActorsPreparator.Prepare();
            DynamicEndStep.End();
        }
    }
}