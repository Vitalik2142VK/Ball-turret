using System;

public class Level : ILevel
{
    private ILevelActorsPlanner _actorsPlanner;

    public Level(ILevelActorsPlanner actorsPlanner, float actorsHealthCoefficient, int countCoinsWin = CoinCountRandomizer.DefaultMinCoinsWin, int countCoinsDefeat = CoinCountRandomizer.DefaultMinCoinsDefeat, int index = ILevel.LearningLevelIndex)
    {
        if (actorsHealthCoefficient < 0f)
            throw new ArgumentOutOfRangeException("The coefficient cannot be less than 0");

        if (countCoinsWin <= 0 || countCoinsDefeat <= 0)
            throw new ArgumentOutOfRangeException("The number of coins cannot be equal to or less than 0");

        if (countCoinsWin < countCoinsDefeat)
            throw new ArgumentOutOfRangeException("The number of coins for a victory cannot be less than for a defeat");

        if (index < ILevel.LearningLevelIndex)
            throw new ArgumentOutOfRangeException(nameof(index));

        _actorsPlanner = actorsPlanner ?? throw new ArgumentNullException(nameof(actorsPlanner));
        CurrentWaveNumber = 0;

        ActorsHealthCoefficient = actorsHealthCoefficient;
        CountCoinsWin = countCoinsWin;
        CountCoinsDefeat = countCoinsDefeat;
        Index = index;
    }

    public float ActorsHealthCoefficient { get; }
    public int CountCoinsWin { get; }
    public int CountCoinsDefeat { get; }
    public int Index { get; }
    public int CurrentWaveNumber { get; private set; }

    public int PassedWavesNumber => CurrentWaveNumber - 1;
    public bool AreWavesOver => _actorsPlanner.CountWaves <= CurrentWaveNumber;

    public bool TryGetNextWaveActorsPlanner(out IWaveActorsPlanner waveActorsPlanner)
    {
        waveActorsPlanner = null;

        if (AreWavesOver == false)
        {
            waveActorsPlanner = _actorsPlanner.GetWaveActorsPlanner(++CurrentWaveNumber);

            return true;
        }

        return false;
    }
}
