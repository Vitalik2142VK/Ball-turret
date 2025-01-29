using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Tower _tower;

    private TargetPoint _torgetPoint;

    public event Action GunFired;

    private void OnValidate()
    {
        if (_tower == null)
            throw new NullReferenceException(nameof(_tower));
    }

    public void SetTargetPoint(TargetPoint targetPoint)
    {
        if (targetPoint == null)
            throw new ArgumentNullException(nameof(targetPoint));

        if (_torgetPoint == null)
            _torgetPoint = targetPoint;
    }

    public void SetTouchPoint(Vector3 touchPoint)
    {
        _torgetPoint.SetPosition(touchPoint);  
        _tower.SetTargertPosition(_torgetPoint.Position);
    }

    public void FixTargetPostion()
    {
        if (_torgetPoint.IsInsideZoneEnemy)
        {
            _tower.Shoot();

            GunFired?.Invoke();
        }

        _torgetPoint.SaveLastPosition();
        _tower.SetTargertPosition(_torgetPoint.Position);
    }
}
