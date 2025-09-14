using System;

namespace PlayLevel
{
    public class FullHealthTurretBonusConfigurator : BonusConfigurator
    {
        private IHealth _turretHealth;

        public void SetHealthTurret(IHealth turretHealth)
        {
            _turretHealth = turretHealth ?? throw new ArgumentNullException(nameof(turretHealth));
        }

        public override void Configure()
        {
            FullHealthTurretBonusActivator activator = new FullHealthTurretBonusActivator(_turretHealth);
            BonusPrefab.Initialize(activator);
        }
    }
}
