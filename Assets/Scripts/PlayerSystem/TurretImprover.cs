using System;

public class TurretImprover : ITurretImprover
{
    private const float DefaultCoefficient = 1f;

    private IImprovementTurretAttributes _improvementAttributes;

    public TurretImprover(IImprovementTurretAttributes improvementAttributes, float healthCoefficient = DefaultCoefficient, float damageCoefficient = DefaultCoefficient)
    {
        if (healthCoefficient < DefaultCoefficient)
            throw new ArgumentOutOfRangeException(nameof(healthCoefficient));

        if (damageCoefficient < DefaultCoefficient)
            throw new ArgumentOutOfRangeException(nameof(damageCoefficient));

        _improvementAttributes = improvementAttributes ?? throw new ArgumentNullException(nameof(improvementAttributes));

        HealthCoefficient = healthCoefficient;
        DamageCoefficient = damageCoefficient;
    }

    public float HealthCoefficient { get; private set; }
    public float DamageCoefficient { get; private set; }
    public int LevelHealthImprovement => MathTool.GetStepIndex(HealthCoefficient, DefaultCoefficient, _improvementAttributes.MaxHealthCoefficient, ImproveHealthCoefficient);
    public int LevelDamageImprovement => MathTool.GetStepIndex(DamageCoefficient, DefaultCoefficient, _improvementAttributes.MaxDamageCoefficient, ImproveDamageCoefficient);

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
}