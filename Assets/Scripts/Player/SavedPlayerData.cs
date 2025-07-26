using System;
using System.Collections.Generic;
using YG;

public class SavedPlayerData : ISavedPlayerData
{
    private SavesYG _savesYG;

    public SavedPlayerData()
    {
        if (YG2.isSDKEnabled == false)
            throw new InvalidOperationException("The Yandex SDK is not Enabled");

        _savesYG = YG2.saves;
        _savesYG.CheckPurchaseAvailability();
    }

    public IReadOnlyCollection<IPlayerPurchase> OneTimePurchases => _savesYG.GetOneTimePurchases();
    public float HealthCoefficient => _savesYG.HealthCoefficient;
    public float DamageCoefficient => _savesYG.DamageCoefficient;
    public long CountCoins => _savesYG.CountCoins;
    public int AchievedLevelIndex => _savesYG.AchievedLevelIndex;

    public void SetHealthCoefficient(float healthCoefficient)
    {
        if (healthCoefficient < _savesYG.HealthCoefficient)
            throw new ArgumentOutOfRangeException(nameof(healthCoefficient));

        _savesYG.HealthCoefficient = healthCoefficient;
    }

    public void SetDamageCoefficient(float damageCoefficient)
    {
        if (damageCoefficient < _savesYG.DamageCoefficient)
            throw new ArgumentOutOfRangeException(nameof(damageCoefficient));

        _savesYG.DamageCoefficient = damageCoefficient;
    }

    public void SetCountCoins(long countCoins)
    {
        if (countCoins < 0)
            throw new ArgumentOutOfRangeException(nameof(countCoins));

        _savesYG.CountCoins = countCoins;
    }

    public void SetAchievedLevelIndex(int achievedLevelIndex)
    {
        int nextLevelIndex = 1;
        nextLevelIndex += _savesYG.AchievedLevelIndex;

        if (achievedLevelIndex > nextLevelIndex || achievedLevelIndex < _savesYG.AchievedLevelIndex)
            throw new ArgumentOutOfRangeException(nameof(achievedLevelIndex));

        _savesYG.AchievedLevelIndex = achievedLevelIndex;
    }

    public void RemoveProgerss()
    {
        SavesYG clearSave = new SavesYG();
        _savesYG.HealthCoefficient = clearSave.HealthCoefficient;
        _savesYG.DamageCoefficient = clearSave.DamageCoefficient;
        _savesYG.CountCoins = clearSave.CountCoins;
        _savesYG.AchievedLevelIndex = clearSave.AchievedLevelIndex;

        YG2.SaveProgress();
    }
}
