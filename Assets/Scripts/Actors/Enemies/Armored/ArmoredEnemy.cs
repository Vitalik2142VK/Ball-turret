using CannonTurret.DamageSystem;
using CannonTurret.Effects;
using CannonTurret.HealthSystem;
using System;
using UnityEngine;

namespace CannonTurret.Actors.Enemies.Armored
{
    public class ArmoredEnemy : IEnemy, IArmoredObject
    {
        private readonly IEnemy Enemy;
        private readonly IArmor Armor;

        public ArmoredEnemy(IEnemy enemy, IArmor armor)
        {
            Enemy = enemy ?? throw new ArgumentNullException(nameof(enemy));
            Armor = armor ?? throw new ArgumentNullException(nameof(armor));
        }

        public bool IsEnable => Enemy.IsEnable;
        public bool IsFinished => Enemy.IsFinished;

        public void ActivateDebuffs() => Enemy.ActivateDebuffs();

        public void AddDebuff(IDebuff debaff) => Enemy.AddDebuff(debaff);

        public void ApplyDamage(IDamagedObject damagedObject) => Enemy.ApplyDamage(damagedObject);

        public void SetStartPosition(Vector3 startPosition) => Enemy.SetStartPosition(startPosition);

        public void EstablishPoint(Vector3 distance, float speed) => Enemy.EstablishPoint(distance, speed);

        public void Move() => Enemy.Move();

        public void Destroy() => Enemy.Destroy();

        public void Win() => Enemy.Win();

        public void TakeDamage(IDamageAttributes damage)
        {
            if (damage == null)
                throw new ArgumentNullException(nameof(damage));

            Armor.ReduceDamage(damage);
        }

        public void IgnoreArmor(IDamageAttributes attributes)
        {
            Enemy.TakeDamage(attributes);
        }
    }
}