using System;

namespace CannonTurret.Actors.Enemies
{
    public class EnemiesController : IEnemiesController
    {
        private readonly IAdvancedActorsPreparator ActorsPreparator;
        private readonly IEnemiesAttacker EnemyAttacker;

        public EnemiesController(IAdvancedActorsPreparator actorsPreparator, IEnemiesAttacker enemyAttacker)
        {
            ActorsPreparator = actorsPreparator ?? throw new ArgumentNullException(nameof(actorsPreparator));
            EnemyAttacker = enemyAttacker ?? throw new ArgumentNullException(nameof(enemyAttacker));
        }

        public bool AreNoEnemies => ActorsPreparator.EnemiesCount == 0;

        public void AttackAll() => EnemyAttacker.AttackAll();

        public void Count() => ActorsPreparator.CountRemainingEnemies();

        public void WinAll()
        {
            var enemies = ActorsPreparator.GetEnemies();

            foreach (var enemy in enemies)
                enemy.Win();
        }
    }
}