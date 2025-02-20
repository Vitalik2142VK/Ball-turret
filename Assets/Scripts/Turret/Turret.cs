using System;
using UnityEngine;

public class Turret : ITurret
{
    private IGun _gun;
    private ITower _tower;
    private IHealth _health;

    public bool IsInProcessShooting => _gun.IsShooting;

    public Turret(IGun gun, ITower tower, IHealth health)
    {
        _gun = gun ?? throw new ArgumentNullException(nameof(gun));
        _tower = tower ?? throw new ArgumentNullException(nameof(tower));
        _health = health ?? throw new ArgumentNullException(nameof(health));
    }

    public void SetTouchPoint(Vector3 touchPoint)
    {
        _tower.TakeAim(touchPoint);  
    }

    public void FixTargetPostion()
    {
        if (_tower.IsReadyShoot)
            _gun.Shoot(_tower.Direction);

        _tower.SaveDirection();
    }

    public void Restore()
    {
        _health.Restore();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"Turret.TakeDamage({damage})");

        _health.TakeDamage(damage);

        if (_health.IsAlive == false)
            Destroy();
    }

    public void Destroy()
    {
        Debug.Log("Turret is destroy");
    }
}
