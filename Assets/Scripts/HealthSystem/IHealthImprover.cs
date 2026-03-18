namespace CannonTurret.HealthSystem
{
    public interface IHealthImprover : IHealthAttributes
    {
        public void Improve(float healthCoefficient);
    }
}