using CannonTurret.DamageSystem;

namespace CannonTurret.Actors
{
    public interface IArmoredObject
    {
        public void IgnoreArmor(IDamageAttributes damage);
    }
}