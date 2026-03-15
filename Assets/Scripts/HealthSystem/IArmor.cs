using CannonTurret.DamageSystem;

namespace CannonTurret.HealthSystem
{
    public interface IArmor
    {
        public void ReduceDamage(IDamageAttributes attributes);
    }
}