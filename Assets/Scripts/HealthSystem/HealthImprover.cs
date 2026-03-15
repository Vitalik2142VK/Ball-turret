using System;

public class HealthImprover : IHealthImprover
{
    private const float MinHealthСoefficient = 1f;

    private float _maxHealth;

    public HealthImprover(IHealthAttributes attributes)
    {
        if (attributes == null)
            throw new ArgumentNullException(nameof(attributes));

        _maxHealth = attributes.MaxHealth;
    }

    public float MaxHealth => _maxHealth;

    public void Improve(float healthCoefficient)
    {
        if (healthCoefficient < MinHealthСoefficient)
            throw new ArgumentOutOfRangeException(nameof(healthCoefficient));

        _maxHealth *= healthCoefficient;
    }
}