using CannonTurret.Utils;
using System;

namespace CannonTurret.PlayerSystem
{
    public class TurretImprover : ITurretImprover
    {
        private const float DefaultCoefficient = 1f;

        private readonly IImprovementTurretAttributes ImprovementAttributes;

        public TurretImprover(IImprovementTurretAttributes improvementAttributes, float healthCoefficient = DefaultCoefficient, float damageCoefficient = DefaultCoefficient)
        {
            if (healthCoefficient < DefaultCoefficient)
                throw new ArgumentOutOfRangeException(nameof(healthCoefficient));

            if (damageCoefficient < DefaultCoefficient)
                throw new ArgumentOutOfRangeException(nameof(damageCoefficient));

            ImprovementAttributes = improvementAttributes ?? throw new ArgumentNullException(nameof(improvementAttributes));

            HealthCoefficient = healthCoefficient;
            DamageCoefficient = damageCoefficient;
        }

        public float HealthCoefficient { get; private set; }

        public float DamageCoefficient { get; private set; }

        public int LevelHealthImprovement => MathTool.GetStepIndex(HealthCoefficient, DefaultCoefficient, ImprovementAttributes.MaxHealthCoefficient, ImproveHealthCoefficient);

        public int LevelDamageImprovement => MathTool.GetStepIndex(DamageCoefficient, DefaultCoefficient, ImprovementAttributes.MaxDamageCoefficient, ImproveDamageCoefficient);

        public float ImproveHealthCoefficient => ImprovementAttributes.ImproveHealthCoefficient;

        public float ImproveDamageCoefficient => ImprovementAttributes.ImproveDamageCoefficient;

        public bool CanImproveHealth => ImprovementAttributes.MaxHealthCoefficient > HealthCoefficient;

        public bool CanImproveDamage => ImprovementAttributes.MaxDamageCoefficient > DamageCoefficient;

        public void ImproveHealth()
        {
            if (CanImproveHealth == false)
                throw new InvalidOperationException(nameof(HealthCoefficient));

            HealthCoefficient += ImprovementAttributes.ImproveHealthCoefficient;
        }

        public void ImproveDamage()
        {
            if (CanImproveDamage == false)
                throw new InvalidOperationException(nameof(DamageCoefficient));

            DamageCoefficient += ImprovementAttributes.ImproveDamageCoefficient;
        }
    }
}