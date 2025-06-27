using System;
using UnityEngine;

public class Tower : MonoBehaviour, ITower
{
    private const float MaxDistance = 100f;
    private const float CoefficientOffsetTorget = 0.5f;

    [SerializeField] private Muzzle _muzzle;
    [SerializeField] private LayerMask _layerMask;

    private ITargetPoint _targetPoint;
    private ITrajectoryRenderer _trajectoryRenderer;
    private Transform _transform;
    private Vector3 _touchPosition;

    public Vector3 Direction => _muzzle.Direction;
    public bool IsReadyShoot => _targetPoint.IsInsideZoneEnemy;

    private void OnValidate()
    {
        if (_muzzle == null)
            _muzzle = GetComponentInChildren<Muzzle>();

        if (_muzzle == null)
            throw new NullReferenceException(nameof(_muzzle));
    }

    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        LookAtTarget();
    }

    public void Initialize(ITargetPoint targetPoint, ITrajectoryRenderer trajectoryRenderer)
    {
        _targetPoint = targetPoint ?? throw new ArgumentNullException(nameof(targetPoint));
        _trajectoryRenderer = trajectoryRenderer ?? throw new ArgumentNullException(nameof(trajectoryRenderer));
        _trajectoryRenderer.ShowTrajectory(_muzzle.Position, Direction);
    }

    public void TakeAim(Vector3 targertPosition)
    {
        _touchPosition = targertPosition;
        _trajectoryRenderer.Enable();
        _trajectoryRenderer.ShowTrajectory(_muzzle.Position, Direction);
    }

    public void SaveDirection(Vector3 targertPosition)
    {
        _touchPosition = targertPosition;
        _trajectoryRenderer.Disable();
        _targetPoint.SaveLastPosition();

        LookAtTarget();
    }

    private void LookAtTarget()
    {

        Vector3 direction = (_touchPosition - _muzzle.Position);

        if (Physics.Raycast(_muzzle.Position, direction, out RaycastHit hitInfo, MaxDistance, _layerMask))
        {
            Vector3 offset = (hitInfo.point - _transform.position).normalized * CoefficientOffsetTorget;

            _targetPoint.SetPosition(hitInfo.point - offset);
        }
        else
        {
            if (_targetPoint.Position == _touchPosition)
                return;

            _targetPoint.SetPosition(_touchPosition);
        }

        Rotate();
    }

    private void Rotate()
    {
        Vector3 direction = _targetPoint.Position - _transform.position;
        Vector3 horizontalDirection = new Vector3(direction.x, 0, direction.z);
        Vector3 localTargetPosition = _transform.InverseTransformPoint(_targetPoint.Position);

        _transform.rotation = Quaternion.LookRotation(horizontalDirection);
        _muzzle.LookAtTarget(localTargetPosition);
    }
}
