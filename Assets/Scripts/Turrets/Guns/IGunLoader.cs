using CannonTurret.Turrets.Bullets;

namespace CannonTurret.Turrets.Guns
{
    public interface IGunLoader
    {
        public void AddBullet(IBullet bullet);
    }
}