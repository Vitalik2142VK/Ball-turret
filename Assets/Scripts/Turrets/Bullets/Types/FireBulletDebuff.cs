using CannonTurret.Actors;
using CannonTurret.DamageSystem;
using CannonTurret.Effects;
using System;
using UnityEngine;

namespace CannonTurret.Turrets.Bullets.Types
{
    public class FireBulletDebuff : MonoBehaviour, IBulletDebuff
    {
        private const float FireDamageCoefficient = 1.5f;

        private IDamageAttributes _damageAttributes;

        public void Initialize(IDamageAttributes attributes)
        {
            if (attributes == null)
                throw new ArgumentNullException(nameof(attributes));

            _damageAttributes = new DamageAttributes(attributes.Damage * FireDamageCoefficient);
        }

        public void ApplyDebuff(IDebuffReceiver debuffsReceiver)
        {
            if (debuffsReceiver == null)
                throw new ArgumentNullException(nameof(debuffsReceiver));

            if (debuffsReceiver is IDamagedObject damagedObject)
            {
                FireDebuff fireDebuff = new FireDebuff(damagedObject, _damageAttributes);
                debuffsReceiver.AddDebuff(fireDebuff);
            }
        }
    }
}