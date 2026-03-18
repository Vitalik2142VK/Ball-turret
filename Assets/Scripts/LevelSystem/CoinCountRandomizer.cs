using System;

namespace CannonTurret.LevelSystem
{
    public class CoinCountRandomizer : ICoinCountRandomizer
    {
        private const float DefaultCoinsRewardCoefficient = 3f;
        private const float DefaultCoefficient = 1f;
        private const float CoinCoefficientByLevel = 0.45f;
        private const int DefaultCoinsWin = 1000;
        private const int DefaultCoinsWave = 100;
        private const int DefaultCoinsWinOffset = 200;
        private const int DefaultCoinsWaveOffset = 50;

        private readonly Random Random;
        private readonly float CoinsForRewardAdCoefficient;
        private readonly int CurrenMaxLevelPlayer;

        public CoinCountRandomizer(int currentMaxLevelPlayer = 0, float coinsForRewardAdCoefficient = DefaultCoinsRewardCoefficient)
        {
            if (currentMaxLevelPlayer < 0)
                throw new ArgumentOutOfRangeException(nameof(currentMaxLevelPlayer));

            if (DefaultCoefficient < 0)
                throw new ArgumentOutOfRangeException(nameof(coinsForRewardAdCoefficient));

            Random = new Random();
            CurrenMaxLevelPlayer = currentMaxLevelPlayer;
            CoinsForRewardAdCoefficient = coinsForRewardAdCoefficient;
        }

        public int CountCoinsForRewardAd => (int)(DefaultCoinsWin * CoinsForRewardAdCoefficient * CalculateCoefficient(CurrenMaxLevelPlayer));

        public int GetCountCoinsForWin(int indexLevel)
        {
            int maxCoinsWin = DefaultCoinsWinOffset + DefaultCoinsWin;
            maxCoinsWin = (int)(maxCoinsWin * CalculateCoefficient(indexLevel));
            int minCoinsWin = (int)(DefaultCoinsWin * CalculateCoefficient(indexLevel));

            return Random.Next(minCoinsWin, ++maxCoinsWin);
        }

        public int GetCountCoinsForWave(int indexLevel)
        {
            int maxCoinsWin = DefaultCoinsWaveOffset + DefaultCoinsWave;
            maxCoinsWin = (int)(maxCoinsWin * CalculateCoefficient(indexLevel));
            int minCoinsWin = (int)(DefaultCoinsWave * CalculateCoefficient(indexLevel));

            return Random.Next(minCoinsWin, ++maxCoinsWin);
        }

        private float CalculateCoefficient(int indexLevel)
        {
            return DefaultCoefficient + CoinCoefficientByLevel * indexLevel;
        }
    }
}