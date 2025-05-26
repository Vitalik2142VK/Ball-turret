using System;
using UnityEngine;

public class Tower : MonoBehaviour, ITower
{
    private const float MaxDistance = 100f;
    private const float CoefficientOffsetTorget = 0.5f;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private TrajectoryRenderer _trajectoryRenderer;

    private ITargetPoint _targetPoint;
    private Transform _transform;
    private Vector3 _touchPosition;

    public Vector3 Direction => _transform.forward;
    public bool IsReadyShoot => _targetPoint.IsInsideZoneEnemy;

    private void Awake()
    {
        if (_trajectoryRenderer == null)
            throw new NullReferenceException(nameof(_trajectoryRenderer));

        _transform = transform;
        _trajectoryRenderer.Disable();
    }

    private void FixedUpdate()
    {
        LookAtTarget();
    }

    public void Initialize(ITargetPoint targetPoint)
    {
        _targetPoint = targetPoint ?? throw new ArgumentNullException(nameof(targetPoint));
        _trajectoryRenderer.ShowTrajectory(_transform.position, _transform.forward);
    }

    public void TakeAim(Vector3 touchPosition)
    {
        _touchPosition = touchPosition;
        _trajectoryRenderer.Enable();
        _trajectoryRenderer.ShowTrajectory(_transform.position, _transform.forward);
    }

    public void SaveDirection()
    {
        _trajectoryRenderer.Disable();
        _targetPoint.SaveLastPosition();

        LookAtTarget();
    }

    private void LookAtTarget()
    {
        Vector3 direction = (_touchPosition - _transform.position);

        if (Physics.Raycast(_transform.position, direction, out RaycastHit hitInfo, MaxDistance, _layerMask))
        {
            Vector3 offset = (hitInfo.point - _transform.position).normalized * CoefficientOffsetTorget;

            _targetPoint.SetPosition(hitInfo.point - offset);
        }
        else
        {
            _targetPoint.SetPosition(_touchPosition);
        }

        _transform.LookAt(_targetPoint.Position);

    }
}
