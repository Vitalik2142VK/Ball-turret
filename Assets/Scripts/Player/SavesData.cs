using System;
using YG;

public class SavesData : ISavesData
{
    private SavesYG _savesYG;

    public SavesData()
    {
        if (YandexGame.SDKEnabled == false)
            throw new InvalidOperationException("The Yandex SDK is not Enabled");

        _savesYG = YandexGame.savesData;
    }

    public float HealthCoefficient => _savesYG.HealthCoefficient;
    public float DamageCoefficient => _savesYG.DamageCoefficient;
    public int CountCoins => _savesYG.CountCoins;
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

    public void SetCountCoins(int countCoins)
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
}