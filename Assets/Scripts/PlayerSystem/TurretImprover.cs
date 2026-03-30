using CannonTurret.Utils;
using System;

namespace CannonTurret.PlayerSystem
{
    public class TurretImprover : ITurretImprover
    {
        private const float DefaultCoefficient = 1f;

        private readonly IImprovementTurretAttributes _improvementAttributes;

        public TurretImprover(
            IImprovementTurretAttributes improvementAttributes,
            float healthCoefficient = DefaultCoefficient,
            float damageCoefficient = DefaultCoefficient)
        {
            if (healthCoefficient < DefaultCoefficient)
                throw new ArgumentOutOfRangeException(nameof(healthCoefficient));

            if (damageCoefficient < DefaultCoefficient)
                throw new ArgumentOutOfRangeException(nameof(damageCoefficient));

            if (improvementAttributes == null)
                throw new ArgumentNullException(nameof(improvementAttributes));

            _improvementAttributes = improvementAttributes;

            HealthCoefficient = healthCoefficient;
            DamageCoefficient = damageCoefficient;
        }

        public float HealthCoefficient { get; private set; }

        public float DamageCoefficient { get; private set; }

        public float ImproveHealthCoefficient => _improvementAttributes.ImproveHealthCoefficient;

        public float ImproveDamageCoefficient => _improvementAttributes.ImproveDamageCoefficient;

        public bool CanImproveHealth => _improvementAttributes.MaxHealthCoefficient > HealthCoefficient;

        public bool CanImproveDamage => _improvementAttributes.MaxDamageCoefficient > DamageCoefficient;

        public void ImproveHealth()
        {
            if (CanImproveHealth == false)
                throw new InvalidOperationException(nameof(HealthCoefficient));

            HealthCoefficient += _improvementAttributes.ImproveHealthCoefficient;
        }

        public void ImproveDamage()
        {
            if (CanImproveDamage == false)
                throw new InvalidOperationException(nameof(DamageCoefficient));

            DamageCoefficient += _improvementAttributes.ImproveDamageCoefficient;
        }

        public int GetLevelHealthImprovement()
        {
            return MathTool.GetStepIndex(
                HealthCoefficient, 
                DefaultCoefficient, 
                _improvementAttributes.MaxHealthCoefficient, 
                ImproveHealthCoefficient);
        }

        public int GetLevelDamageImprovement()
        {
            return MathTool.GetStepIndex(
                DamageCoefficient, 
                DefaultCoefficient, 
                _improvementAttributes.MaxDamageCoefficient, 
                ImproveDamageCoefficient);
        }
    }
}