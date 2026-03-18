using CannonTurret.Turrets.Bullets;

namespace CannonTurret.Turrets.Guns
{
    public class GunMagazine : IGunMagazine
    {
        private readonly IBulletRepository BulletRepository;

        public GunMagazine(IBulletRepository bulletRepository)
        {
            BulletRepository = bulletRepository ?? throw new System.ArgumentNullException(nameof(bulletRepository));
        }

        public bool HasFreeBullets => BulletRepository.HasFreeBullets;

        public bool IsFull => BulletRepository.AreBulletsReturned;

        public void AddBullet(IBullet bullet)
        {
            BulletRepository.Add(bullet);
        }

        public IBullet GetBullet()
        {
            return BulletRepository.Get();
        }
    }
}