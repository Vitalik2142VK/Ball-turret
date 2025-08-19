using System;

public class CoinCountRandomizer : ICoinCountRandomizer
{
    private const int DefaultCoinsWin = 1000;
    private const int DefaultCoinsWave = 200;
    private const float DefaultCoefficient = 1f;
    private const float CoinCoefficientByLevel = 0.4f;
    private const int DefaultCoinsWinOffset = 100;
    private const int DefaultCoinsWaveOffset = 50;

    private Random _random;
    private int _currenMaxLevelPlayer;

    public CoinCountRandomizer(int currentMaxLevelPlayer = 0)
    {
        if (currentMaxLevelPlayer < 0)
            throw new ArgumentOutOfRangeException(nameof(currentMaxLevelPlayer));

        _random = new Random();
        _currenMaxLevelPlayer = currentMaxLevelPlayer;
    }

    public int CountCoinsForRewardAd => DefaultCoinsWin * _currenMaxLevelPlayer;

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