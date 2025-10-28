using System;
using UnityEngine;

namespace PlayLevel
{
    public class BulletConfigurator : MonoBehaviour
    {
        [SerializeField] private BulletFactory _bulletFactory;
        [SerializeField] private ComboCounter _comboCounter;
        [SerializeField] private Sound _hitBulletSound;
        [SerializeField] private Scriptable.DamageAttributes _damageBulletAttributes;

        [Header("Exploding bullet prefab")]
        [SerializeField] private BulletsCollector _bulletsCollector;
        [SerializeField] private ExplodingBullet _explodingBulletPrefab;
        [SerializeField] private Sound _explosionSound;

        public IBulletFactory BulletFactory => _bulletFactory;

        private void OnValidate()
        {
            if (_bulletFactory == null)
                throw new NullReferenceException(nameof(_bulletFactory));

            if (_comboCounter == null)
                throw new NullReferenceException(nameof(_comboCounter));

            if (_hitBulletSound == null)
                throw new NullReferenceException(nameof(_hitBulletSound));

            if (_damageBulletAttributes == null)
                throw new NullReferenceException(nameof(_damageBulletAttributes));

            if (_bulletsCollector == null)
                throw new NullReferenceException(nameof(_bulletsCollector));

            if (_explodingBulletPrefab == null)
                throw new NullReferenceException(nameof(_explodingBulletPrefab));

            if (_explosionSound == null)
                throw new NullReferenceException(nameof(_explosionSound));
        }

        public void Configure(IPlayer player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            var damageChanger = new DamageChanger(_damageBulletAttributes);
            damageChanger.Change(player.DamageCoefficient);

            _bulletFactory.Initialize(damageChanger, _comboCounter, _hitBulletSound);
            _explodingBulletPrefab.SetExplosionSound(_explosionSound);
            _explodingBulletPrefab.SetBulletRepository(_bulletsCollector);

            if (_explodingBulletPrefab.TryGetComponent(out Bullet bullet))
                _bulletFactory.AddPrefab(bullet);
        }
    }
}
