using CannonTurret.Actors;

namespace CannonTurret.Turrets.Bullets
{
    public interface IBulletDebuff
    {
        public void ApplyDebuff(IDebuffReceiver debuffReceiver);
    }
}