using System;

public class DamageImprover : IDamageImprover
{
    private IDamageAttributes _attributes;
    private float _damage;

    public DamageImprover(IDamageAttributes attributes)
    {
        _attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
        _damage = _attributes.Damage;
    }

    public float Damage => _damage;

    public void Improve(IDamageImproverAttributes damageImproverAttributes)
    {
        if (damageImproverAttributes == null)
            throw new ArgumentNullException(nameof(damageImproverAttributes));

        _damage *= damageImproverAttributes.DamageСoefficient;
    }
}
