using UnityEngine;

namespace PlayLevel
{
    [RequireComponent(typeof(Exploder))]
    public class BigBangBonusConfigurator : BonusConfigurator
    {
        [Header("Explosion")]
        [SerializeField] private Player _player;
        [SerializeField] private Transform _pointExplosion;
        [SerializeField] private Scriptable.DamageAttributes _explosionDamageAttributes;

        private IExploder _exploder;
        private Vector3 _pointExplosionPosition;

        private void OnValidate()
        {
            if (_pointExplosion == null)
                throw new System.NullReferenceException(nameof(_pointExplosion));
        }

        private void Awake()
        {
            Exploder exploder = GetComponent<Exploder>();
            DamageImprover improvingDamage = new DamageImprover(_explosionDamageAttributes);
            float damageCoefficient = _player.User.BulletDamageCoefficient;
            improvingDamage.Improve(damageCoefficient);
            exploder.Initialize(improvingDamage);

            _exploder = exploder;
            _pointExplosionPosition = transform.position;
        }

        public override void Configure()
        {
            BigBangBonusActivator activator = new BigBangBonusActivator(_exploder, _pointExplosionPosition);

            BonusPrefab.Initialize(activator);
        }
    }
}
