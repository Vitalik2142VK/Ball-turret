using System;

public class DamageChanger : IDamageChanger
{
    private const float MinDamageСoefficient = 0.1f;

    private float _damage;

    public DamageChanger(IDamageAttributes attributes)
    {
        if (attributes == null)
            throw new ArgumentNullException(nameof(attributes));

        _damage = attributes.Damage;
    }

    public float Damage => _damage;

    public void Change(IDamageImproverAttributes damageImproverAttributes)
    {
        if (damageImproverAttributes == null)
            throw new ArgumentNullException(nameof(damageImproverAttributes));

        _damage *= damageImproverAttributes.DamageСoefficient;
    }

    public void Change(float damageСoefficient)
    {
        if (damageСoefficient < MinDamageСoefficient)
            throw new ArgumentOutOfRangeException(nameof(damageСoefficient));

        _damage *= damageСoefficient;
    }
}
