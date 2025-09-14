using UnityEngine;

namespace PlayLevel
{
    [RequireComponent(typeof(Exploder))]
    public class BigBangBonusConfigurator : BonusConfigurator
    {
        [Header("Explosion")]
        [SerializeField] private Scriptable.CachedPlayer _player;
        [SerializeField] private Scriptable.DamageAttributes _explosionDamageAttributes;
        [SerializeField] private Transform _pointExplosion;
        [SerializeField] private Sound _bigBangSound;
        [SerializeField] private ExplosionView _explosionView;

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
            DamageChanger damageChanger = new DamageChanger(_explosionDamageAttributes);
            float damageCoefficient = _player.DamageCoefficient;
            damageChanger.Change(damageCoefficient);
            exploder.Initialize(damageChanger, _bigBangSound, _explosionView);

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
