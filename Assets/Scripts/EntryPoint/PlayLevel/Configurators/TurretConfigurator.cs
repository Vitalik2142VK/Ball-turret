using System;
using UnityEngine;
using Scriptable;

namespace PlayLevel
{
    public class TurretConfigurator : MonoBehaviour
    {
        [Header("Turret")]
        [SerializeField] private Tower _tower;
        [SerializeField] private TargetPoint _targetPoint;
        [SerializeField] private DefaultGun _gun;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private TrajectoryRenderer _trajectoryRenderer;
        [SerializeField] private Sound _shootSound;

        [Header("Bullets")]
        [SerializeField] private BulletsCollector _bulletCollector;

        [Header("Attributes")]
        [SerializeField] private GunAttributes _gunAttributes;
        [SerializeField] private HealthAttributes _turretHealthAttributes;

        private Turret _turret;
        private WinState _winState;

        public ITurret Turret => _turret;
        public IWinState WinState => _winState;

        private void OnValidate()
        {
            if (_tower == null)
                throw new NullReferenceException(nameof(_tower));

            if (_targetPoint == null)
                throw new NullReferenceException(nameof(_targetPoint));

            if (_gun == null)
                throw new NullReferenceException(nameof(_gun));

            if (_healthBar == null)
                throw new NullReferenceException(nameof(_healthBar));

            if (_trajectoryRenderer == null)
                throw new NullReferenceException(nameof(_trajectoryRenderer));

            if (_shootSound == null)
                throw new NullReferenceException(nameof(_shootSound));

            if (_bulletCollector == null)
                throw new NullReferenceException(nameof(_bulletCollector));

            if (_gunAttributes == null)
                throw new NullReferenceException(nameof(_gunAttributes));

            if (_turretHealthAttributes == null)
                throw new NullReferenceException(nameof(_turretHealthAttributes));
        }

        private void OnDisable()
        {
            _turret.Disable();
        }

        public void Configure(IPlayer user, IBulletFactory bulletFactory)
        {
            if (bulletFactory == null)
                throw new NullReferenceException(nameof(bulletFactory));

            IGunMagazine gunMagazine = CreateGunMagazine(bulletFactory);

            _gun.Initialize(gunMagazine, _shootSound, _gunAttributes.TimeBetweenShots);
            _tower.Initialize(_targetPoint, _trajectoryRenderer);

            IHealthImprover healthImprover = new HealthImprover(_turretHealthAttributes);
            healthImprover.Improve(user.HealthCoefficient);
            IHealth health = new Health(healthImprover, _healthBar);
            health.Restore();
            _winState = new WinState();

            _turret = new Turret(_gun, _tower, health, _winState);
            _turret.Enable();
        }

        private IGunMagazine CreateGunMagazine(IBulletFactory bulletFactory)
        {
            int initialCountBullets = _gunAttributes.InitialCountBulltes;
            GunMagazine magazine = new GunMagazine(_bulletCollector);

            for (int i = 0; i < initialCountBullets; i++)
            {
                IBullet bullet = bulletFactory.Create(BulletType.Default);

                magazine.AddBullet(bullet);
            }

            return magazine;
        }
    }
}
