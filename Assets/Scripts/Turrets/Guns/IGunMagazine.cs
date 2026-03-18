using CannonTurret.Turrets.Bullets;

namespace CannonTurret.Turrets.Guns
{
    public interface IGunMagazine
    {
        public bool HasFreeBullets { get; }

        public bool IsFull { get; }

        public void AddBullet(IBullet bullet);

        public IBullet GetBullet();
    }
}