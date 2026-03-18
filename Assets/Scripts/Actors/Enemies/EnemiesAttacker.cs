using CannonTurret.DamageSystem;
using System;
using System.Collections.Generic;

namespace CannonTurret.Actors.Enemies
{
    public class EnemiesAttacker : IEnemiesAttacker, IAttackingEnemiesCollector
    {
        private readonly List<IEnemy> AttackingEnemies;
        private readonly IDamagedObject DamagedObject;

        public EnemiesAttacker(IDamagedObject damagedObject)
        {
            DamagedObject = damagedObject ?? throw new ArgumentNullException(nameof(damagedObject));
            AttackingEnemies = new List<IEnemy>();
        }

        public void Add(IEnemy enemy)
        {
            if (enemy == null)
                throw new ArgumentNullException(nameof(enemy));

            AttackingEnemies.Add(enemy);
        }

        public void AttackAll()
        {
            foreach (var enemy in AttackingEnemies)
                enemy.ApplyDamage(DamagedObject);

            AttackingEnemies.Clear();
        }
    }
}