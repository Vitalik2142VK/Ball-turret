using System;
using UnityEngine;

public class Turret : ITurret
{
    private IGun _gun;
    private ITower _tower;
    private IHealth _health;
    private IEndStep _endStep;

    public Turret(IGun gun, ITower tower, IHealth health)
    {
        _gun = gun ?? throw new ArgumentNullException(nameof(gun));
        _tower = tower ?? throw new ArgumentNullException(nameof(tower));
        _health = health ?? throw new ArgumentNullException(nameof(health));
    }

    public event Action Destroyed;

    public bool IsReadyShoot => _gun.IsRecharged;

    public void Enable()
    {
        _gun.ShootingCompleted += OnEndStep;
    }

    public void Disable()
    {
        _gun.ShootingCompleted -= OnEndStep;
    }

    public void SetEndStep(IEndStep endStep)
    {
        _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
    }

    public void SetTouchPoint(Vector3 touchPoint)
    {
        _tower.TakeAim(touchPoint);  
        _gun.TakeAim(_tower.Direction);
    }

    public void FixTargetPostion()
    {
        if (_tower.IsReadyShoot)
            _gun.Shoot(_tower.Direction);

        _tower.SaveDirection();
    }

    public void TakeDamage(IDamageAttributes damage)
    {
        _health.TakeDamage(damage);

        if (_health.IsAlive == false)
            Destroy();
    }

    public void Destroy()
    {
        Destroyed?.Invoke();
    }

    public void RestoreHealth()
    {
        _health.Restore();
    }

    private void OnEndStep()
    {
        _endStep.End();
    }
}
