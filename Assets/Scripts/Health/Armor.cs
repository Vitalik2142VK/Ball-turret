using System;

public class Armor : IArmor
{
    private IDamagedObject _armoredDamagedObject;
    private IArmorAttributes _armorAttributes;

    public Armor(IDamagedObject armoredDamagedObject, IArmorAttributes armorAttributes)
    {
        _armoredDamagedObject = armoredDamagedObject ?? throw new ArgumentNullException(nameof(armoredDamagedObject));
        _armorAttributes = armorAttributes ?? throw new ArgumentNullException(nameof(armorAttributes));
    }

    public void ReduceDamage(IDamageAttributes attributes)
    {
        var damageChanger = new DamageChanger(attributes);
        damageChanger.Change(_armorAttributes.DamageReductionCoefficient);
        _armoredDamagedObject.TakeDamage(damageChanger);
    }
}
