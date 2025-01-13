using UnityEngine;

public class TargetPoint : MonoBehaviour 
{
    [SerializeField] private ZoneEnemy _zoneEnemy;

    [Header("Debug")]
    [SerializeField] private bool _isDebugOn = false;
    [SerializeField] private Color _color = Color.red;
    [SerializeField, Min(0.1f)] private float _radusSphere = 1f;

    private Transform _transform;
    private Vector3 _startPosition;

    public bool IsInsideZoneEnemy { get; private set; } = true;

    public Vector3 Position => _transform.localPosition;

    private void OnValidate()
    {
        if (_zoneEnemy == null)
            throw new System.NullReferenceException(nameof(_zoneEnemy));
    }

    private void Awake()
    {
        _transform = transform;
        _startPosition = _transform.localPosition;
    }

    private void OnEnable()
    {
        _zoneEnemy.TargetPointExited += OnReturnToStartPosotion;
    }

    private void OnDrawGizmos()
    {
        if (_isDebugOn)
        {
            Gizmos.color = _color;
            Gizmos.DrawWireSphere(transform.position, _radusSphere);
        }
    }
    private void OnDisable()
    {
        _zoneEnemy.TargetPointExited -= OnReturnToStartPosotion;
    }

    public void SetPosition(Vector3 position)
    {
        if (IsInsideZoneEnemy)
            _transform.localPosition = new Vector3(position.x, _startPosition.y, position.z);
    }

    public void SaveLastPosition()
    {
        _startPosition = _transform.localPosition;
        IsInsideZoneEnemy = true;
    }

    private void OnReturnToStartPosotion()
    {
        IsInsideZoneEnemy = false;
        _transform.localPosition = _startPosition;
    }
}
