using System;

namespace PlayLevel
{
    public class FullHealthTurretBonusConfigurator : BonusConfigurator
    {
        private ITurret _turret;

        public void SetHealthTurret(ITurret turret)
        {
            _turret = turret ?? throw new ArgumentNullException(nameof(turret));
        }

        public override void Configure()
        {
            FullHealthTurretBonusActivator activator = new FullHealthTurretBonusActivator(_turret);
            BonusPrefab.Initialize(activator);
        }
    }
}
