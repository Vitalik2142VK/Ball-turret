using System;

public class DamageImprovementProduct : IImprovementProduct
{
    private const int Remains = 2;

    private ITurretImprover _turretImprover;
    private float _damage;

    public DamageImprovementProduct(ITurretImprover turretImprover, float damage)
    {
        if (damage <= 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        _turretImprover = turretImprover ?? throw new ArgumentNullException(nameof(turretImprover));
        _damage = damage;
    }

    public float ImproveValue => (float)Math.Round(_damage * _turretImprover.ImproveDamageCoefficient, Remains);
    public int CurrentValue => (int)Math.Round(_damage * _turretImprover.DamageCoefficient);
    public bool CanImprove => _turretImprover.CanImproveDamage;
}
