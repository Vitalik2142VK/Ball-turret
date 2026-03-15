public interface ITurretImprover
{
    public float HealthCoefficient { get; }
    public float DamageCoefficient { get; }
    public float ImproveHealthCoefficient { get; }
    public float ImproveDamageCoefficient { get; }
    public int LevelHealthImprovement { get; }
    public int LevelDamageImprovement { get; }
    public bool CanImproveHealth { get; }
    public bool CanImproveDamage { get; }

    public void ImproveHealth();

    public void ImproveDamage();
}
