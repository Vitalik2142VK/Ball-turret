using System;

public class FullHealthTurretBonusActivator : IBonusActivator
{
    private IHealth _turretHealth;
    private IBonusActicatorView _view;

    public FullHealthTurretBonusActivator(IHealth turretHealth, IBonusActicatorView view)
    {
        _turretHealth = turretHealth ?? throw new ArgumentNullException(nameof(turretHealth));
        _view = view ?? throw new ArgumentNullException(nameof(view));
    }

    public void Activate()
    {
        _turretHealth.Restore();
        _view.PlayActivation();
    }
}