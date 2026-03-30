namespace CannonTurret.PlayerSystem
{
    public interface ITurretImprover
    {
        public float HealthCoefficient { get; }

        public float DamageCoefficient { get; }

        public float ImproveHealthCoefficient { get; }

        public float ImproveDamageCoefficient { get; }

        public bool CanImproveHealth { get; }

        public bool CanImproveDamage { get; }

        public void ImproveHealth();

        public void ImproveDamage();

        public int GetLevelHealthImprovement();

        public int GetLevelDamageImprovement();
    }
}