using System;

public class CoinCountRandomizer : ICoinCountRandomizer
{
    public const int DefaultMinCoinsWin = 1000;
    public const int DefaultMinCoinsDefeat = 200;

    private const float DefaultCoefficient = 1f;
    private const float CoinCoefficientByLevel = 0.4f;
    private const int DefaultMaxCoinsWin = 1100;
    private const int DefaultMaxCoinsDefeat = 250;

    private Random _random;
    private int _currenMaxLevelPlayer;

    public CoinCountRandomizer(int currentMaxLevelPlayer)
    {
        int learningLevelIndex = ILevel.LearningLevelIndex;

        if (currentMaxLevelPlayer < learningLevelIndex)
            throw new ArgumentOutOfRangeException(nameof(currentMaxLevelPlayer));
        
        if (learningLevelIndex == currentMaxLevelPlayer)
            currentMaxLevelPlayer = 0;

        _random = new Random();
        _currenMaxLevelPlayer = currentMaxLevelPlayer;
    }

    public int CountCoinsForRewardAd => (int)(DefaultMinCoinsWin * CalculateCoefficient(_currenMaxLevelPlayer));

    public int GetCountCoinsWin(int indexLevel)
    {
        int maxCoinsWin = (int)(DefaultMaxCoinsWin * CalculateCoefficient(indexLevel));
        int minCoinsWin = (int)(DefaultMinCoinsWin * CalculateCoefficient(indexLevel));

        return _random.Next(minCoinsWin, ++maxCoinsWin);
    }

    public int GetCountCoinsDefeat(int indexLevel)
    {
        int maxCoinsWin = (int)(DefaultMaxCoinsDefeat * CalculateCoefficient(indexLevel));
        int minCoinsWin = (int)(DefaultMinCoinsDefeat * CalculateCoefficient(indexLevel));

        return _random.Next(minCoinsWin, ++maxCoinsWin);
    }

    private float CalculateCoefficient(int indexLevel)
    {
        return DefaultCoefficient + CoinCoefficientByLevel * indexLevel;
    }
}