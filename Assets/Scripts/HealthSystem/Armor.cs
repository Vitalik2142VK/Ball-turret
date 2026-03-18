using CannonTurret.DamageSystem;
using System;

namespace CannonTurret.HealthSystem
{
    public class Armor : IArmor
    {
        private readonly IDamagedObject ArmoredDamagedObject;
        private readonly IArmorAttributes ArmorAttributes;

        public Armor(IDamagedObject armoredDamagedObject, IArmorAttributes armorAttributes)
        {
            ArmoredDamagedObject = armoredDamagedObject ?? throw new ArgumentNullException(nameof(armoredDamagedObject));
            ArmorAttributes = armorAttributes ?? throw new ArgumentNullException(nameof(armorAttributes));
        }

        public void ReduceDamage(IDamageAttributes attributes)
        {
            var damageChanger = new DamageChanger(attributes);
            damageChanger.Change(ArmorAttributes.DamageReductionCoefficient);
            ArmoredDamagedObject.TakeDamage(damageChanger);
        }
    }
}