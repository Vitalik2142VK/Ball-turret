using CannonTurret.DamageSystem;
using System;

namespace CannonTurret.HealthSystem
{
    public class Armor : IArmor
    {
        private readonly IDamagedObject _armoredDamagedObject;
        private readonly IArmorAttributes _armorAttributes;

        public Armor(IDamagedObject armoredDamagedObject, IArmorAttributes armorAttributes)
        {
            if (armoredDamagedObject == null)
                throw new ArgumentNullException(nameof(armoredDamagedObject));

            if (armorAttributes == null)
                throw new ArgumentNullException(nameof(armorAttributes));

            _armoredDamagedObject = armoredDamagedObject;
            _armorAttributes = armorAttributes;
        }

        public void ReduceDamage(IDamageAttributes attributes)
        {
            var damageChanger = new DamageChanger(attributes);
            damageChanger.Change(_armorAttributes.DamageReductionCoefficient);
            _armoredDamagedObject.TakeDamage(damageChanger);
        }
    }
}