using System;

public class User : IUser
{
    private const float DefaultCoefficient = 1f;

    private ITurretImprover _turretImprover;

    public User(ITurretImprover turretImprover, IWallet wallet)
    {
        _turretImprover = turretImprover ?? throw new ArgumentNullException(nameof(turretImprover));

        Wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));

        TurretHealthCoefficient = DefaultCoefficient;
        BulletDamageCoefficient = DefaultCoefficient;
        AchievedLevelIndex = 0;
        AreAdsDisabled = false;
    }

    public User(ITurretImprover turretImprover, IWallet wallet, float turretHealthCoefficient, float bulletDamageCoefficient, int achievedLevel, bool areAdsDisabled)
    {
        if (turretHealthCoefficient < DefaultCoefficient)
            throw new ArgumentOutOfRangeException(nameof(turretHealthCoefficient));

        if (bulletDamageCoefficient < DefaultCoefficient)
            throw new ArgumentOutOfRangeException(nameof(bulletDamageCoefficient));

        if (bulletDamageCoefficient < DefaultCoefficient)
            throw new ArgumentOutOfRangeException(nameof(bulletDamageCoefficient));

        _turretImprover = turretImprover ?? throw new ArgumentNullException(nameof(turretImprover));

        Wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));

        TurretHealthCoefficient = turretHealthCoefficient;
        BulletDamageCoefficient = bulletDamageCoefficient;
        AchievedLevelIndex = achievedLevel;
        AreAdsDisabled = areAdsDisabled;
    }

    public IWallet Wallet { get; private set; }
    public float TurretHealthCoefficient { get; private set; }
    public float BulletDamageCoefficient { get; private set; }
    public int AchievedLevelIndex { get; private set; }
    public bool AreAdsDisabled { get; private set; }

    public bool CanImproveHealthCoefficient => TurretHealthCoefficient < _turretImprover.MaxHealthCoefficient;
    public bool CanImproveDamageCoefficient => BulletDamageCoefficient < _turretImprover.MaxDamageCoefficient;

    public void ImproveTurret()
    {
        if (CanImproveHealthCoefficient == false)
            return;

        TurretHealthCoefficient += _turretImprover.ImproveHealthCoefficient;
    }

    public void ImproveBullet()
    {
        if (CanImproveDamageCoefficient == false)
            return;

        BulletDamageCoefficient += _turretImprover.ImproveDamageCoefficient;
    }

    public void IncreaseAchievedLevel()
    {
        AchievedLevelIndex++;
    }

    public void DisabledAds()
    {
        AreAdsDisabled = true;
    }
}
