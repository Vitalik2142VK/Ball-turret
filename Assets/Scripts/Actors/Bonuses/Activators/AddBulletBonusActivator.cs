using CannonTurret.Turrets.Bullets;
using CannonTurret.Turrets.Bullets.Creation;
using CannonTurret.Turrets.Bullets.Types;
using CannonTurret.Turrets.Guns;
using System;

namespace CannonTurret.Actors.Bonuses.Activators
{
    public class AddBulletBonusActivator : IBonusActivator
    {
        private IBulletFactory _bulletFactory;
        private IGunLoader _gunLoader;
        private BulletType _bulletType;

        public AddBulletBonusActivator(IBulletFactory bulletFactory, IGunLoader gunLoader, BulletType bulletType)
        {
            _bulletFactory = bulletFactory ?? throw new ArgumentNullException(nameof(bulletFactory));
            _gunLoader = gunLoader ?? throw new ArgumentNullException(nameof(gunLoader));
            _bulletType = bulletType;
        }

        public void Activate()
        {
            IBullet bullet = _bulletFactory.Create(_bulletType);
            _gunLoader.AddBullet(bullet);
        }
    }
}