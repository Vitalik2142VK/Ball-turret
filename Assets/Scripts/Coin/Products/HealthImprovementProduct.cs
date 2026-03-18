using CannonTurret.PlayerSystem;
using System;

namespace CannonTurret.Coin.Products
{
    public class HealthImprovementProduct : IImprovementProduct
    {
        private const int Remains = 2;

        private readonly ITurretImprover TurretImprover;
        private readonly float Heath;

        public HealthImprovementProduct(ITurretImprover turretImprover, float heath)
        {
            if (heath <= 0)
                throw new ArgumentOutOfRangeException(nameof(heath));

            TurretImprover = turretImprover ?? throw new ArgumentNullException(nameof(turretImprover));
            Heath = heath;
        }

        public float ImproveValue => (float)Math.Round(Heath * TurretImprover.ImproveHealthCoefficient, Remains);

        public int CurrentValue => (int)Math.Round(Heath * TurretImprover.HealthCoefficient);

        public bool CanImprove => TurretImprover.CanImproveHealth;
    }
}