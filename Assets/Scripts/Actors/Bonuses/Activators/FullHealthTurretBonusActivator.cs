using CannonTurret.HealthSystem;
using System;

namespace CannonTurret.Actors.Bonuses.Activators
{
    public class FullHealthTurretBonusActivator : IBonusActivator
    {
        private readonly IHealth TurretHealth;
        private readonly IBonusActicatorView View;

        public FullHealthTurretBonusActivator(IHealth turretHealth, IBonusActicatorView view)
        {
            TurretHealth = turretHealth ?? throw new ArgumentNullException(nameof(turretHealth));
            View = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void Activate()
        {
            TurretHealth.Restore();
            View.PlayActivation();
        }
    }
}