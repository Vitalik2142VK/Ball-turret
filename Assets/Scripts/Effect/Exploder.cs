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

    public void Initialize(IDamageAttributes attributes, ISound sound)
    {
        if (attributes == null)
            throw new ArgumentNullException(nameof(attributes));

        _sound = sound ?? throw new ArgumentNullException(nameof(sound)); ;
        _damage = new Damage(attributes);
    }

    public void Explode(Vector3 pointContact)
    {
        _sound.Play();

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