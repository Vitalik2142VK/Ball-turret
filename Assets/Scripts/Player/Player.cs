using System;

public class Player : IPlayer
{
    public Player(IWallet wallet, ITurretImprover turretImprover, IPurchasesStorage purchasesStorage, int achievedLevel = 0)
    {
        if (achievedLevel < 0)
            throw new ArgumentOutOfRangeException(nameof(achievedLevel));

        Wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
        TurretImprover = turretImprover ?? throw new ArgumentNullException(nameof(turretImprover));
        PurchasesStorage = purchasesStorage ?? throw new ArgumentNullException(nameof(purchasesStorage));

        AchievedLevelIndex = achievedLevel;
    }

    public float HealthCoefficient => TurretImprover.HealthCoefficient;
    public float DamageCoefficient => TurretImprover.DamageCoefficient;

    public IWallet Wallet { get; }
    public ITurretImprover TurretImprover { get; }
    public IPurchasesStorage PurchasesStorage { get; private set; }
    public int AchievedLevelIndex { get; private set; }

    public void IncreaseAchievedLevel()
    {
        AchievedLevelIndex++;
    }

    public void SetPurchasesStorage(IPurchasesStorage purchasesStorage)
    {
        PurchasesStorage = purchasesStorage ?? throw new ArgumentNullException(nameof(purchasesStorage));
    }
}
