namespace RecorderLevel
{
    public class DamageAttributes : IDamageAttributes
    {
        public DamageAttributes(float damage)
        {
            if (damage <= 0)
                throw new System.ArgumentOutOfRangeException(nameof(damage));

            Damage = damage;
        }

        public float Damage { get; }
    }
}
