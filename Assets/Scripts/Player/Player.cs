using System;

public class Player : IPlayer
{
    public Player(ITurretImprover turretImprover, IWallet wallet, int achievedLevel = 0, bool areAdsDisabled = false)
    {
        if (achievedLevel < 0)
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
