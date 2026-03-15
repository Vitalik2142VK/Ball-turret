using CannonTurret.DamageSystem;

namespace CannonTurret.Actors.Enemies.Armored
{
    public interface IArmoredEnemyPresenter
    {
        public void IgnoreArmor(IDamageAttributes damage);
    }
}