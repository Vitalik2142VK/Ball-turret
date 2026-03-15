using System;

public class HealthImprovementProduct : IImprovementProduct
{
    private const int Remains = 2;

    private ITurretImprover _turretImprover;
    private float _heath;

    public HealthImprovementProduct(ITurretImprover turretImprover, float heath)
    {
        if (heath <= 0)
            throw new ArgumentOutOfRangeException(nameof(heath));

        _turretImprover = turretImprover ?? throw new ArgumentNullException(nameof(turretImprover));
        _heath = heath;
    }

    public float ImproveValue => (float)Math.Round(_heath * _turretImprover.ImproveHealthCoefficient, Remains);
    public int CurrentValue => (int)Math.Round(_heath * _turretImprover.HealthCoefficient);
    public bool CanImprove => _turretImprover.CanImproveHealth;
}