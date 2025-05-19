using System;

public class DamageImprover : IDamageImprover
{
    private const float MinDamageСoefficient = 1f;

    //private IDamageAttributes _attributes;
    private float _damage;

    public DamageImprover(IDamageAttributes attributes)
    {
        if (attributes == null)
            throw new ArgumentNullException(nameof(attributes));

        //_attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
        //_damage = _attributes.Damage;

        _damage = attributes.Damage;
    }

    public float Damage => _damage;

    public void Improve(IDamageImproverAttributes damageImproverAttributes)
    {
        if (damageImproverAttributes == null)
            throw new ArgumentNullException(nameof(damageImproverAttributes));

        _damage *= damageImproverAttributes.DamageСoefficient;
    }

    public void Improve(float damageСoefficient)
    {
        if (damageСoefficient < MinDamageСoefficient)
            throw new ArgumentOutOfRangeException(nameof(damageСoefficient));

        _damage *= damageСoefficient;
    }
}
