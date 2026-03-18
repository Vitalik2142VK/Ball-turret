using CannonTurret.AudioSystem;
using CannonTurret.DamageSystem;
using System;
using UnityEngine;

namespace CannonTurret.Effects
{
    public class Exploder : MonoBehaviour, IExploder
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField, Min(1f)] private float _explosionRadius;
        [SerializeField][Range(1, 64)] private int _countColliders = 16;

        [Header("Debug")]
        [SerializeField] private bool _isDebugOn = false;

        private IDamage _damage;
        private ISound _sound;
        private IExplosionView _explosionView;
        private Collider[] _colliders;

        public void Initialize(IDamageAttributes attributes, ISound sound, IExplosionView explosionView)
        {
            if (attributes == null)
                throw new ArgumentNullException(nameof(attributes));

            _sound = sound ?? throw new ArgumentNullException(nameof(sound));
            _explosionView = explosionView ?? throw new ArgumentNullException(nameof(explosionView));
            _damage = new Damage(attributes);

            _colliders = new Collider[_countColliders];
        }

        public void Explode(Vector3 pointContact)
        {
            _sound.Play();
            _explosionView.Play();

            int count = Physics.OverlapSphereNonAlloc(pointContact, _explosionRadius, _colliders, _layerMask, QueryTriggerInteraction.Ignore);

            for (int i = 0; i < count; i++)
            {
                Collider collider = _colliders[i];

                if (collider.TryGetComponent(out IDamagedObject damagedObject))
                    _damage.Apply(damagedObject);
            }
        }

        private void OnDrawGizmos()
        {
            if (_isDebugOn == false)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _explosionRadius);
        }
    }
}