using System;

public class Damage : IDamage
{
    private readonly IDamageAttributes Attributes;

    public Damage(IDamageAttributes attributes)
    {
        Attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
    }

    public void Apply(IDamagedObject damagedObject)
    {
        if (damagedObject == null)
            throw new ArgumentNullException(nameof(damagedObject));

        damagedObject.TakeDamage(Attributes.Damage);
    }
}