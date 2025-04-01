using System;

public class DamageImprover : IDamageImprover
{
    private IDamageAttributes _defalutDamageImprover;
    private IDamageAttributes _damageAttributes;

    public DamageImprover(IDamageAttributes attributes)
    {
        _defalutDamageImprover = attributes ?? throw new ArgumentNullException(nameof(attributes));
    }

    public int Damage => _defalutDamageImprover.Damage + _damageAttributes.Damage;

    public void Improve(IDamageAttributes attributes)
    {
        _damageAttributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
    }
}