using Scriptable;
using System;
using UnityEngine;

namespace RecorderLevel
{
    public class TurretConfigurator : MonoBehaviour
    {
        [Header("Turret")]
        [SerializeField] private Tower _tower;
        [SerializeField] private TargetPoint _targetPoint;
        [SerializeField] private Gun _gun;
        [SerializeField] private RecordingTurretView _turretView;

        [SerializeField, SerializeIterface(typeof(ITrajectoryRenderer))] private GameObject _trajectoryRendererGameObject;

        [Header("Bullets")]
        [SerializeField] private Bullet[] _bullets;
        [SerializeField] private BulletsCollector _bulletCollector;
        [SerializeField] private Sound _hitBulletSound;
        [SerializeField, Min(20f)] private float _damage;

        [Header("Attributes")]
        [SerializeField] private GunAttributes _gunAttributes;

        private ITrajectoryRenderer _trajectoryRenderer;
        private RecordingTurret _turret;

        public ITurret Turret => _turret;

        private void OnValidate()
        {
            if (_tower == null)
                throw new NullReferenceException(nameof(_tower));

            if (_targetPoint == null)
                throw new NullReferenceException(nameof(_targetPoint));

            if (_gun == null)
                throw new NullReferenceException(nameof(_gun));

            if (_turretView == null)
                throw new NullReferenceException(nameof(_turretView));

            if (_trajectoryRendererGameObject == null)
                throw new NullReferenceException(nameof(_trajectoryRendererGameObject));

            if (_bullets == null || _bullets.Length == 0)
                throw new InvalidOperationException(nameof(_bullets));

            foreach (var bullet in _bullets)
                if (bullet == null)
                    throw new NullReferenceException($"{_bullets} contains null objects");

            if (_bulletCollector == null)
                throw new NullReferenceException(nameof(_bulletCollector));

            if (_hitBulletSound == null)
                throw new NullReferenceException(nameof(_hitBulletSound));

            if (_gunAttributes == null)
                throw new NullReferenceException(nameof(_gunAttributes));
        }

        private void Awake()
        {
            _trajectoryRenderer = _trajectoryRendererGameObject.GetComponent<ITrajectoryRenderer>();
        }

        public void Configure()
        {
            IGunMagazine gunMagazine = CreateGunMagazine();

            _gun.Initialize(gunMagazine, _gunAttributes.TimeBetweenShots);
            _tower.Initialize(_targetPoint, _trajectoryRenderer);

            _turret = new RecordingTurret(_turretView, _gun, _tower);
            _turret.Enable();
        }

        private IGunMagazine CreateGunMagazine()
        {
            GunMagazine magazine = new GunMagazine(_bulletCollector);
            DamageAttributes damage = new DamageAttributes(_damage);

            foreach (var bullet in _bullets)
            {
                bullet.Initialize(damage, _hitBulletSound);
                bullet.SetActive(false);
                magazine.AddBullet(bullet);
            }

            return magazine;
        }
    }
}
