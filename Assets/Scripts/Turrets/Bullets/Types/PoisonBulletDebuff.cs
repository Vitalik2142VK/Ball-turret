using CannonTurret.Actors;
using CannonTurret.DamageSystem;
using CannonTurret.Effects;
using System;
using UnityEngine;

namespace CannonTurret.Turrets.Bullets.Types
{
    public class PoisonBulletDebuff : MonoBehaviour, IBulletDebuff
    {
        private const float PoisonDamageCoefficient = 0.5f;

        private IDamageAttributes _damageAttributes;

        public void Initialize(IDamageAttributes attributes)
        {
            if (attributes == null)
                throw new ArgumentNullException(nameof(attributes));

            _damageAttributes = new DamageAttributes(attributes.Damage * PoisonDamageCoefficient);
        }

        public void ApplyDebuff(IDebuffReceiver debuffsReceiver)
        {
            if (debuffsReceiver == null)
                throw new ArgumentNullException(nameof(debuffsReceiver));

            if (debuffsReceiver is IDamagedObject damagedObject)
            {
                PoisonDebuff poisonDebuff = new PoisonDebuff(damagedObject, _damageAttributes);
                debuffsReceiver.AddDebuff(poisonDebuff);
            }
        }
    }
}