using System;

public class CoinCountRandomizer : ICoinCountRandomizer
{
    private const float DefaultCoinsRewardCoefficient = 3f;
    private const float DefaultCoefficient = 1f;
    private const float CoinCoefficientByLevel = 0.3f;
    private const int DefaultCoinsWin = 1000;
    private const int DefaultCoinsWave = 100;
    private const int DefaultCoinsWinOffset = 200;
    private const int DefaultCoinsWaveOffset = 50;

    private Random _random;
    private float _coinsForRewardAdCoefficient;
    private int _currenMaxLevelPlayer;

    public CoinCountRandomizer(int currentMaxLevelPlayer = 0, float coinsForRewardAdCoefficient = DefaultCoinsRewardCoefficient)
    {
        if (currentMaxLevelPlayer < 0)
            throw new ArgumentOutOfRangeException(nameof(currentMaxLevelPlayer));

        if (DefaultCoefficient < 0)
            throw new ArgumentOutOfRangeException(nameof(coinsForRewardAdCoefficient));

        _random = new Random();
        _currenMaxLevelPlayer = currentMaxLevelPlayer;
        _coinsForRewardAdCoefficient = coinsForRewardAdCoefficient;
    }

    public int CountCoinsForRewardAd => (int)(GetCountCoinsForWin(_currenMaxLevelPlayer) * _coinsForRewardAdCoefficient);

    public int GetCountCoinsForWin(int indexLevel)
    {
        int maxCoinsWin = DefaultCoinsWinOffset + DefaultCoinsWin;
        maxCoinsWin = (int)(maxCoinsWin * CalculateCoefficient(indexLevel));
        int minCoinsWin = (int)(DefaultCoinsWin * CalculateCoefficient(indexLevel));

        return _random.Next(minCoinsWin, ++maxCoinsWin);
    }

    public int GetCountCoinsForWave(int indexLevel)
    {
        int maxCoinsWin = DefaultCoinsWaveOffset + DefaultCoinsWave;
        maxCoinsWin = (int)(maxCoinsWin * CalculateCoefficient(indexLevel));
        int minCoinsWin = (int)(DefaultCoinsWave * CalculateCoefficient(indexLevel));

        return _random.Next(minCoinsWin, ++maxCoinsWin);
    }

    private float CalculateCoefficient(int indexLevel)
    {
        return DefaultCoefficient + CoinCoefficientByLevel * indexLevel;
    }
}