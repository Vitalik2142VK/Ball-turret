using System;

public class FullHealthTurretBonusActivator : IBonusActivator
{
    private IHealth _turretHealth;

    public FullHealthTurretBonusActivator(IHealth turretHealth)
    {
        _turretHealth = turretHealth ?? throw new ArgumentNullException(nameof(turretHealth));
    }

    public void Activate()
    {
        _turretHealth.Restore();
    }
}