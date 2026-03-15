using System.Collections.Generic;

public interface ISavedPlayerData
{
    public IReadOnlyCollection<IPlayerPurchase> OneTimePurchases {  get; }
    public float HealthCoefficient { get; }
    public float DamageCoefficient { get; }
    public long CountCoins { get; }
    public int AchievedLevelIndex { get; }

    public void SetHealthCoefficient(float healthCoefficient);

    public void SetDamageCoefficient(float damageCoefficient);

    public void SetCountCoins(long countCoins);

    public void SetAchievedLevelIndex(int achievedLevelIndex);
}
