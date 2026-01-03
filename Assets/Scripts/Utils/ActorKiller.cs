using UnityEngine;

//todo Remove on realise
public class ActorKiller : MonoBehaviour
{
    [SerializeField, Min(1f)] private float _killRadius;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private KeyCode _killKey = KeyCode.R;
    [SerializeField] private bool _isShowRaius = false;

    private IDamage _damage;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;

        DamageAttributes damageAttributes = new DamageAttributes(int.MaxValue);
        _damage = new Damage(damageAttributes);
    }

    private void Update()
    {
        if (Input.GetKeyUp(_killKey))
            KillAll();
    }

    private void OnDrawGizmos()
    {
        if (_isShowRaius == false)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _killRadius);
    }

    private void KillAll()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _killRadius, _layerMask, QueryTriggerInteraction.Ignore);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out IDamagedObject damagedObject))
                _damage.Apply(damagedObject);
        }
    }
}
