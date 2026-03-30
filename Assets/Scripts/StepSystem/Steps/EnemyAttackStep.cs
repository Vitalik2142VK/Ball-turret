using CannonTurret.Actors.Enemies;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class EnemyAttackStep : IStep, IEndPointStep
    {
        private readonly IEnemiesAttacker _enemiesAttacker;

        private IEndStep _endStep;

        public EnemyAttackStep(IEnemiesAttacker enemiesAttacker)
        {
            _enemiesAttacker = enemiesAttacker ?? throw new ArgumentNullException(nameof(enemiesAttacker));
        }

        public void Action()
        {
            _enemiesAttacker.AttackAll();
            _endStep.End();
        }

        public void SetEndStep(IEndStep endStep)
        {
            _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
        }
    }
}