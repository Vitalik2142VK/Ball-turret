using CannonTurret.Actors.Enemies;
using System;

namespace CannonTurret.StepSystem.Steps
{
    public class EnemyAttackStep : IStep, IEndPointStep
    {
        private readonly IEnemiesAttacker EnemiesAttacker;

        private IEndStep _endStep;

        public EnemyAttackStep(IEnemiesAttacker enemiesAttacker)
        {
            EnemiesAttacker = enemiesAttacker ?? throw new ArgumentNullException(nameof(enemiesAttacker));
        }

        public void Action()
        {
            EnemiesAttacker.AttackAll();
            _endStep.End();
        }

        public void SetEndStep(IEndStep endStep)
        {
            _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
        }
    }
}