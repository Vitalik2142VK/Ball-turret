using System;
using UnityEngine;

public class FullHealthTurretActivatorCreator : MonoBehaviour, IBonusActivatorCreator
{
    [SerializeField] private FullHealthTurretView _fullHealthTurretView;

    private IHealth _turretHealth;

    private void OnValidate()
    {
        if (_fullHealthTurretView == null)
            throw new NullReferenceException(nameof(_fullHealthTurretView));
    }

    public void SetHealthTurret(IHealth turretHealth)
    {
        _turretHealth = turretHealth ?? throw new ArgumentNullException(nameof(turretHealth));
    }

    public IBonusActivator Create()
    {
        if (_turretHealth == null)
            throw new NullReferenceException(nameof(_turretHealth));

        return new FullHealthTurretBonusActivator(_turretHealth, _fullHealthTurretView);
    }
}
