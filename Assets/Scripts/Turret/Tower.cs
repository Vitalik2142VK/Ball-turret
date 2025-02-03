using UnityEngine;

public class Tower : MonoBehaviour, ITower
{
    private ITargetPoint _targetPoint;
    private Transform _transform;

    public Vector3 Direction => _transform.forward;
    public bool IsReadyShoot => _targetPoint.IsInsideZoneEnemy;

    private void Awake()
    {
        _transform = transform;
    }

    public void Initialize(ITargetPoint targetPoint)
    {
        _targetPoint = targetPoint ?? throw new System.ArgumentNullException(nameof(targetPoint));
    }

    public void SetTargertPosition(Vector3 targertPosition)
    {
        _targetPoint.SetPosition(targertPosition);
        _transform.LookAt(_targetPoint.Position);
    }

    public void SaveDirection()
    {
        _targetPoint.SaveLastPosition();
        _transform.LookAt(_targetPoint.Position);
    }
}
