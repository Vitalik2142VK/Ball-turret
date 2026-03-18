using CannonTurret.Turrets.Bullets.Types;

namespace CannonTurret.Turrets.Bullets.Creation
{
    public interface IBulletFactory
    {
        public IBullet Create(BulletType type);
    }
}