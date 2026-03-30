using CannonTurret.HealthSystem;
using System;

namespace CannonTurret.Actors.Bonuses.Activators
{
    public class FullHealthTurretBonusActivator : IBonusActivator
    {
        private readonly IHealth _turretHealth;
        private readonly IBonusActicatorView _view;

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
}