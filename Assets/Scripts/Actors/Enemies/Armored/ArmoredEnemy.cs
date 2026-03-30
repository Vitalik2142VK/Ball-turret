using CannonTurret.DamageSystem;
using CannonTurret.Effects;
using CannonTurret.HealthSystem;
using System;
using UnityEngine;

namespace CannonTurret.Actors.Enemies.Armored
{
    public class ArmoredEnemy : IEnemy, IArmoredObject
    {
        private readonly IEnemy _enemy;
        private readonly IArmor _armor;

        public ArmoredEnemy(IEnemy enemy, IArmor armor)
        {
            _enemy = enemy ?? throw new ArgumentNullException(nameof(enemy));
            _armor = armor ?? throw new ArgumentNullException(nameof(armor));
        }

        public bool IsEnable => _enemy.IsEnable;
        public bool IsFinished => _enemy.IsFinished;

        public void ActivateDebuffs() => _enemy.ActivateDebuffs();

        public void AddDebuff(IDebuff debaff) => _enemy.AddDebuff(debaff);

        public void ApplyDamage(IDamagedObject damagedObject) => _enemy.ApplyDamage(damagedObject);

        public void SetStartPosition(Vector3 startPosition) => _enemy.SetStartPosition(startPosition);

        public void EstablishPoint(Vector3 distance, float speed) => _enemy.EstablishPoint(distance, speed);

        public void Move() => _enemy.Move();

        public void Destroy() => _enemy.Destroy();

        public void Win() => _enemy.Win();

        public void TakeDamage(IDamageAttributes damage)
        {
            if (damage == null)
                throw new ArgumentNullException(nameof(damage));

            _armor.ReduceDamage(damage);
        }

        public void IgnoreArmor(IDamageAttributes attributes)
        {
            _enemy.TakeDamage(attributes);
        }
    }
}