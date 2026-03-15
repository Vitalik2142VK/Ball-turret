using System;

public class Damage : IDamage
{
    private IDamageAttributes _attributes;

    public Damage(IDamageAttributes attributes)
    {
        _attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
    }

    public void Apply(IDamagedObject damagedObject)
    {
        if (damagedObject == null)
            throw new ArgumentNullException(nameof(damagedObject));

        damagedObject.TakeDamage(_attributes);
    }
}