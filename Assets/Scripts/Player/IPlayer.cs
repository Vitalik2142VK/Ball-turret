public interface IPlayer
{
    public IWallet Wallet { get; }
    public ITurretImprover TurretImprover { get; }
    public IPurchasesStorage PurchasesStorage { get; }
    public float HealthCoefficient { get; }
    public float DamageCoefficient { get; }
    public int AchievedLevelIndex { get; }
    public bool IsLearningComplete { get; }

    public void IncreaseAchievedLevel();
}
