using System;
using UnityEngine;

public class Exploder : MonoBehaviour, IExploder
{
    [SerializeField, Min(1f)] private float _explosionRadius;
    [SerializeField] private LayerMask _layerMask;

    [Header("Debug")]
    [SerializeField] private bool _isDebugOn = false; 

    private IDamage _damage;
    private ISound _sound;
    private IExplosionView _explosionView;

    public void Initialize(IDamageAttributes attributes, ISound sound, IExplosionView explosionView)
    {
        if (attributes == null)
            throw new ArgumentNullException(nameof(attributes));

        _sound = sound ?? throw new ArgumentNullException(nameof(sound));
        _explosionView = explosionView ?? throw new ArgumentNullException(nameof(explosionView));
        _damage = new Damage(attributes);
    }

    public void Explode(Vector3 pointContact)
    {
        _sound.Play();
        _explosionView.Play();

        Collider[] colliders = Physics.OverlapSphere(pointContact, _explosionRadius, _layerMask, QueryTriggerInteraction.Ignore);

        foreach (var collider in colliders)
        {
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
