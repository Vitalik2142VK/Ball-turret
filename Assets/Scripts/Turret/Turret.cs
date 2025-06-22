using System;
using UnityEngine;

public class Turret : ITurret
{
    private IGun _gun;
    private ITower _tower;
    private IHealth _health;
    private IEndStep _endStep;
    private WinState _winState;

    public Turret(IGun gun, ITower tower, IHealth health, WinState winState)
    {
        _gun = gun ?? throw new ArgumentNullException(nameof(gun));
        _tower = tower ?? throw new ArgumentNullException(nameof(tower));
        _health = health ?? throw new ArgumentNullException(nameof(health));
        _winState = winState ?? throw new ArgumentNullException(nameof(winState));
    }

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
        _winState.IsWin = false;
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
