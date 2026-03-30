namespace CannonTurret.PlayerSystem
{
    public interface IImprovementTurretAttributes
    {
        public float MaxHealthCoefficient { get; }

        public float MaxDamageCoefficient { get; }

        public float ImproveHealthCoefficient { get; }

        public float ImproveDamageCoefficient { get; }
    }
}