using System;

public class CoinCountRandomizer : ICoinCountRandomizer
{
    private const float DefaultCoefficient = 1f;
    private const float CoinCoefficientByLevel = 0.4f;
    private const int DefaultCoinsWinOffset = 100;
    private const int DefaultCoinsWaveOffset = 50;

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

    public int CountCoinsForRewardAd => (int)(ICoinCountRandomizer.DefaultCoinsWin * CalculateCoefficient(_currenMaxLevelPlayer));

    public int GetCountCoinsForWin(int indexLevel)
    {
        int maxCoinsWin = DefaultCoinsWinOffset + ICoinCountRandomizer.DefaultCoinsWin;
        maxCoinsWin = (int)(maxCoinsWin * CalculateCoefficient(indexLevel));
        int minCoinsWin = (int)(ICoinCountRandomizer.DefaultCoinsWin * CalculateCoefficient(indexLevel));

        return _random.Next(minCoinsWin, ++maxCoinsWin);
    }

    public int GetCountCoinsForWave(int indexLevel)
    {
        int maxCoinsWin = DefaultCoinsWaveOffset + ICoinCountRandomizer.DefaultCoinsWave;
        maxCoinsWin = (int)(maxCoinsWin * CalculateCoefficient(indexLevel));
        int minCoinsWin = (int)(ICoinCountRandomizer.DefaultCoinsWave * CalculateCoefficient(indexLevel));

        return _random.Next(minCoinsWin, ++maxCoinsWin);
    }

    private float CalculateCoefficient(int indexLevel)
    {
        return DefaultCoefficient + CoinCoefficientByLevel * indexLevel;
    }
}