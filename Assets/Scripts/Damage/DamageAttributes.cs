public class DamageAttributes : IDamageAttributes
{
    public DamageAttributes(float damage)
    {
        Damage = damage;
    }

    public float Damage { get; private set; }
}