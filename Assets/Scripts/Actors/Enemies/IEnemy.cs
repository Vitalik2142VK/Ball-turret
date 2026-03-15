using CannonTurret.DamageSystem;

namespace CannonTurret.Actors.Enemies
{
    public interface IEnemy : IActor, IDamagedObject, IDebuffable
    {
        public void ApplyDamage(IDamagedObject damagedObject);

        public void Win();
    }
}