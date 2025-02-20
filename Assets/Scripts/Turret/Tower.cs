using UnityEngine;

public class Tower : MonoBehaviour, ITower
{
    private const float MaxDistance = 100f;

    [SerializeField] private LayerMask _layerMask;

    private ITargetPoint _targetPoint;
    private Transform _transform;
    private Vector3 _touchPosition;

    public Vector3 Direction => _transform.forward;
    public bool IsReadyShoot => _targetPoint.IsInsideZoneEnemy;

    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        LookAtTarget();
    }

    public void Initialize(ITargetPoint targetPoint)
    {
        _targetPoint = targetPoint ?? throw new System.ArgumentNullException(nameof(targetPoint));
    }

    public void TakeAim(Vector3 touchPosition)
    {
        _touchPosition = touchPosition;
    }

    public void SaveDirection()
    {
        _targetPoint.SaveLastPosition();

        LookAtTarget();
    }

    private void LookAtTarget()
    {
        Vector3 direction = (_touchPosition - _transform.position);

        if (Physics.Raycast(_transform.position, direction, out RaycastHit hitInfo, MaxDistance, _layerMask))
            _targetPoint.SetPosition(hitInfo.point);
        else
            _targetPoint.SetPosition(_touchPosition);

        _transform.LookAt(_targetPoint.Position);
    }
}
