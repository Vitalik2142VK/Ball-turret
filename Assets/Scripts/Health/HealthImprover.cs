using System;

public class HealthImprover : IHealthImprover
{
    private const float MinHealthСoefficient = 1f;

    private float _health;

    public HealthImprover(IHealthAttributes attributes)
    {
        if (attributes == null)
            throw new ArgumentNullException(nameof(attributes));

        _health = attributes.MaxHealth;
    }

    public float MaxHealth => _health;

    public void Improve(float healthCoefficient)
    {
        if (healthCoefficient < MinHealthСoefficient)
            throw new ArgumentOutOfRangeException(nameof(healthCoefficient));

        _health *= healthCoefficient;
    }
}