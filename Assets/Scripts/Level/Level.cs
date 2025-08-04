using System;

public class Level : ILevel
{
    private ILevelActorsPlanner _actorsPlanner;
    private int _countCoinsWave;
    public int _passedWavesNumber;

    public Level(ILevelActorsPlanner actorsPlanner, float actorsHealthCoefficient, int countCoinsWin = ICoinCountRandomizer.DefaultCoinsWin, int countCoinsWave = ICoinCountRandomizer.DefaultCoinsWave, int index = ILevel.LearningLevelIndex)
    {
        if (actorsHealthCoefficient < 0f)
            throw new ArgumentOutOfRangeException("The coefficient cannot be less than 0");

        if (countCoinsWin <= 0 || countCoinsWave <= 0)
            throw new ArgumentOutOfRangeException("The number of coins cannot be equal to or less than 0");

        if (countCoinsWin < countCoinsWave)
            throw new ArgumentOutOfRangeException("The number of coins for a victory cannot be less than for a defeat");

        if (index < ILevel.LearningLevelIndex)
            throw new ArgumentOutOfRangeException(nameof(index));

        _actorsPlanner = actorsPlanner ?? throw new ArgumentNullException(nameof(actorsPlanner));
        _countCoinsWave = countCoinsWave;
        _passedWavesNumber = 0;

        CurrentWaveNumber = 0;
        ActorsHealthCoefficient = actorsHealthCoefficient;
        CountCoinsForWin = countCoinsWin;
        Index = index;
    }

    public float ActorsHealthCoefficient { get; }
    public int CountCoinsForWin { get; }
    public int Index { get; }
    public int CurrentWaveNumber { get; private set; }

    public int CountCoinsForWaves => _countCoinsWave * _passedWavesNumber;
    public bool AreWavesOver => _actorsPlanner.CountWaves <= CurrentWaveNumber;

    public bool TryGetNextWaveActorsPlanner(out IWaveActorsPlanner waveActorsPlanner)
    {
        if (AreWavesOver == false)
        {
            waveActorsPlanner = _actorsPlanner.GetWaveActorsPlanner(++CurrentWaveNumber);
            _passedWavesNumber = CurrentWaveNumber - 1;

            return true;
        }
        else
        {
            waveActorsPlanner = null;
            _passedWavesNumber = _actorsPlanner.CountWaves;

            return false;
        }
    }
}
