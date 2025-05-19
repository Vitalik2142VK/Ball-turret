public interface IUser
{
    public IWallet Wallet { get; }
    public float TurretHealthCoefficient { get; }
    public float BulletDamageCoefficient { get; }
    public int AchievedLevelIndex { get; }
    public bool AreAdsDisabled { get; }

    public void IncreaseAchievedLevel();
}