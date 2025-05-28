using System;

public class CoinCountRandomizer : ICoinCountRandomizer
{
    private const float DefaultCoefficient = 1f;
    private const float CoinCoefficientByLevel = 0.4f;
    private const int DefaultMaxCoinsWin = 1100;
    private const int DefaultMinCoinsWin = 1000;
    private const int DefaultMaxCoinsDefeat = 250;
    private const int DefaultMinCoinsDefeat = 200;

    private Random _random;

    public CoinCountRandomizer()
    {
        _random = new Random();
    }

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