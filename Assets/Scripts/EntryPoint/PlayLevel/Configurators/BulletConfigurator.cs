using System;
using UnityEngine;

namespace PlayLevel
{
    public class BulletConfigurator : MonoBehaviour
    {
        [SerializeField] private BulletFactory _bulletFactory;
        [SerializeField] private Scriptable.DamageAttributes _damageBulletAttributes;

        [Header("Exploding bullet prefab")]
        [SerializeField] private BulletsCollector _bulletsCollector;
        [SerializeField] private ExplodingBullet _explodingBulletPrefab;

        public IBulletFactory BulletFactory => _bulletFactory;

        private void OnValidate()
        {
            if (_bulletFactory == null)
                throw new NullReferenceException(nameof(_bulletFactory));

            if (_damageBulletAttributes == null)
                throw new NullReferenceException(nameof(_damageBulletAttributes));

            if (_bulletsCollector == null)
                throw new NullReferenceException(nameof(_bulletsCollector));

            if (_explodingBulletPrefab == null)
                throw new NullReferenceException(nameof(_explodingBulletPrefab));
        }

        public void Configure(IUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            DamageImprover improveDamageBullet = new DamageImprover(_damageBulletAttributes);
            improveDamageBullet.Improve(user.DamageCoefficient);

            _bulletFactory.Initialize(improveDamageBullet);
            _explodingBulletPrefab.SetBulletRepository(_bulletsCollector);

            if (_explodingBulletPrefab.TryGetComponent(out Bullet bullet))
                _bulletFactory.AddPrefab(bullet);
        }
    }
}
