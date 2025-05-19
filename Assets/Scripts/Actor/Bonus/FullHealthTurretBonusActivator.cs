using System;

public class FullHealthTurretBonusActivator : IBonusActivator
{
    private ITurret _turret;

    public FullHealthTurretBonusActivator(ITurret turret)
    {
        _turret = turret ?? throw new ArgumentNullException(nameof(turret));
    }

    public void Activate()
    {
        _turret.RestoreHealth();
    }

    public IBonusActivator Clone()
    {
        return new FullHealthTurretBonusActivator(_turret);
    }
}