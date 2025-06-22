public interface IPlayer
{
    public IWallet Wallet { get; }
    public ITurretImprover TurretImprover { get; }
    public float HealthCoefficient { get; }
    public float DamageCoefficient { get; }
    public int AchievedLevelIndex { get; }
    public bool AreAdsDisabled { get; }

    public void IncreaseAchievedLevel();
}