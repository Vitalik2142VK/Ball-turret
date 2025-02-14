using System;
using UnityEngine;

public class Turret : ITurret
{
    private IGun _gun;
    private ITower _tower;

    public bool IsInProcessShooting => _gun.IsShooting;

    public Turret(IGun gun, ITower tower)
    {
        _gun = gun ?? throw new ArgumentNullException(nameof(gun));
        _tower = tower ?? throw new ArgumentNullException(nameof(tower));
    }

    public void SetTouchPoint(Vector3 touchPoint)
    {
        _tower.SetTargertPosition(touchPoint);  
    }

    public void FixTargetPostion()
    {
        if (_tower.IsReadyShoot)
            _gun.Shoot(_tower.Direction);

        _tower.SaveDirection();
    }
}
