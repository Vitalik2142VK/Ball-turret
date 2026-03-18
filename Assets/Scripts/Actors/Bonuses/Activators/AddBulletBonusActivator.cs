using CannonTurret.Turrets.Bullets;
using CannonTurret.Turrets.Bullets.Creation;
using CannonTurret.Turrets.Bullets.Types;
using CannonTurret.Turrets.Guns;
using System;

namespace CannonTurret.Actors.Bonuses.Activators
{
    public class AddBulletBonusActivator : IBonusActivator
    {
        private readonly IBulletFactory BulletFactory;
        private readonly IGunLoader GunLoader;
        private readonly BulletType BulletType;

        public AddBulletBonusActivator(IBulletFactory bulletFactory, IGunLoader gunLoader, BulletType bulletType)
        {
            BulletFactory = bulletFactory ?? throw new ArgumentNullException(nameof(bulletFactory));
            GunLoader = gunLoader ?? throw new ArgumentNullException(nameof(gunLoader));
            BulletType = bulletType;
        }

        public void Activate()
        {
            IBullet bullet = BulletFactory.Create(BulletType);
            GunLoader.AddBullet(bullet);
        }
    }
}