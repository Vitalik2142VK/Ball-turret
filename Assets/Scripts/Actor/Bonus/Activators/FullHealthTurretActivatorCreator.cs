using System;
using UnityEngine;

public class FullHealthTurretActivatorCreator : MonoBehaviour, IBonusActivatorCreator
{
    private IHealth _turretHealth;

    public void SetHealthTurret(IHealth turretHealth)
    {
        _turretHealth = turretHealth ?? throw new ArgumentNullException(nameof(turretHealth));
    }

    public IBonusActivator Create()
    {
        if (_turretHealth == null)
            throw new NullReferenceException(nameof(_turretHealth));

        return new FullHealthTurretBonusActivator(_turretHealth);
    }
}
