using CannonTurret.PlayerSystem;
using System;

namespace CannonTurret.Coin.Products
{
    public class DamageImprovementProduct : IImprovementProduct
    {
        private const int Remains = 2;

        private readonly ITurretImprover TurretImprover;
        private readonly float Damage;

        public DamageImprovementProduct(ITurretImprover turretImprover, float damage)
        {
            if (damage <= 0)
                throw new ArgumentOutOfRangeException(nameof(damage));

            TurretImprover = turretImprover ?? throw new ArgumentNullException(nameof(turretImprover));
            Damage = damage;
        }

        public float ImproveValue => (float)Math.Round(Damage * TurretImprover.ImproveDamageCoefficient, Remains);

        public int CurrentValue => (int)Math.Round(Damage * TurretImprover.DamageCoefficient);

        public bool CanImprove => TurretImprover.CanImproveDamage;
    }
}