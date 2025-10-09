using UnityEngine;

public class TargetPoint : MonoBehaviour, ITargetPoint
{
    [SerializeField] private ActorZone _zoneEnemy;

    [Header("Debug")]
    [SerializeField] private bool _isDebugOn = false;
    [SerializeField] private Color _color = Color.red;
    [SerializeField, Min(0.1f)] private float _radusSphere = 1f;

    private Transform _transform;
    private Vector3 _startPosition;

    public bool IsInsideZoneEnemy { get; private set; } = true;

    public Vector3 Position => _transform.position;

    private void OnValidate()
    {
        if (_zoneEnemy == null)
            throw new System.NullReferenceException(nameof(_zoneEnemy));
    }

    private void Awake()
    {
        _transform = transform;
        _startPosition = _transform.position;
    }

    private void OnDrawGizmos()
    {
        if (_isDebugOn)
        {
            Gizmos.color = _color;
            Gizmos.DrawWireSphere(transform.position, _radusSphere);
        }
    }

    public void SetPosition(Vector3 position)
    {
        IsInsideZoneEnemy = _zoneEnemy.IsPointInside(position);

        if (IsInsideZoneEnemy)
            _transform.position = new Vector3(position.x, _startPosition.y, position.z);
        else
            _transform.position = _startPosition;
    }

    public void SaveLastPosition()
    {
        _startPosition = _transform.position;
    }
}
