using UnityEngine;

namespace PlayLevel
{
    [RequireComponent(typeof(Exploder))]
    public class BigBangBonusConfigurator : BonusConfigurator
    {
        [Header("Explosion")]
        [SerializeField] private PlayLevelPlayer _player;
        [SerializeField] private Transform _pointExplosion;
        [SerializeField] private Sound _bigBangSound;
        [SerializeField] private ExplosionView _explosionView;
        [SerializeField] private Scriptable.DamageAttributes _explosionDamageAttributes;

        private IExploder _exploder;
        private Vector3 _pointExplosionPosition;

        private void OnValidate()
        {
            if (_player == null)
                throw new System.NullReferenceException(nameof(_player));

            if (_pointExplosion == null)
                throw new System.NullReferenceException(nameof(_pointExplosion));

            if (_bigBangSound == null)
                throw new System.NullReferenceException(nameof(_bigBangSound));

            if (_explosionView == null)
                throw new System.NullReferenceException(nameof(_explosionView));

            if (_explosionDamageAttributes == null)
                throw new System.NullReferenceException(nameof(_explosionDamageAttributes));
        }

        private void Awake()
        {
            Exploder exploder = GetComponent<Exploder>();
            DamageImprover improvingDamage = new DamageImprover(_explosionDamageAttributes);
            float damageCoefficient = _player.DamageCoefficient;
            improvingDamage.Improve(damageCoefficient);
            exploder.Initialize(improvingDamage, _bigBangSound, _explosionView);

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
