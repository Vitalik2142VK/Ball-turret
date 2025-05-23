using System;

public class User : IUser
{
    public User(ITurretImprover turretImprover, IWallet wallet)
    {
        TurretImprover = turretImprover ?? throw new ArgumentNullException(nameof(turretImprover));

        Wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));

        AchievedLevelIndex = 0;
        AreAdsDisabled = false;
    }

    public User(ITurretImprover turretImprover, IWallet wallet, int achievedLevel, bool areAdsDisabled)
    {
        if (achievedLevel <= 0)
            throw new ArgumentOutOfRangeException(nameof(achievedLevel));

        TurretImprover = turretImprover ?? throw new ArgumentNullException(nameof(turretImprover));

        Wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));

        AchievedLevelIndex = achievedLevel;
        AreAdsDisabled = areAdsDisabled;
    }

    public IWallet Wallet { get; private set; }
    public ITurretImprover TurretImprover { get; private set; }
    public int AchievedLevelIndex { get; private set; }
    public bool AreAdsDisabled { get; private set; }

    public float HealthCoefficient => TurretImprover.HealthCoefficient;
    public float DamageCoefficient => TurretImprover.DamageCoefficient;


    public void IncreaseAchievedLevel()
    {
        AchievedLevelIndex++;
    }

    public void DisabledAds()
    {
        AreAdsDisabled = true;
    }
}
