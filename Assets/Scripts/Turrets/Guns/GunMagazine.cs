using CannonTurret.Turrets.Bullets;

namespace CannonTurret.Turrets.Guns
{
    public class GunMagazine : IGunMagazine
    {
        private readonly IBulletRepository _bulletRepository;

        public GunMagazine(IBulletRepository bulletRepository)
        {
            _bulletRepository = bulletRepository ?? throw new System.ArgumentNullException(nameof(bulletRepository));
        }

        public bool HasFreeBullets => _bulletRepository.HasFreeBullets;

        public bool IsFull => _bulletRepository.AreBulletsReturned;

        public void AddBullet(IBullet bullet)
        {
            _bulletRepository.Add(bullet);
        }

        public IBullet GetBullet()
        {
            return _bulletRepository.Get();
        }
    }
}