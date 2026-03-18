using CannonTurret.DamageSystem;

namespace CannonTurret.HealthSystem
{
    public interface IHealth : IDamagedObject
    {
        public bool IsAlive { get; }

        public void Restore();
    }
}