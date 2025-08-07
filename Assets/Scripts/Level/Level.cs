using System;

public class Level : ILevel
{
    private const float DefaultHealthCoefficient = 1f;

    private ILevelActorsPlanner _actorsPlanner;
    private ICoinCountRandomizer _coinCountRandomizer;
    public int _passedWavesNumber;

    public Level(ILevelActorsPlanner actorsPlanner, ICoinCountRandomizer coinCountRandomizer, float actorsHealthCoefficient = DefaultHealthCoefficient, int index = 0)
    {
        if (actorsHealthCoefficient < 0f)
            throw new ArgumentOutOfRangeException("The coefficient cannot be less than 0");

        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        _actorsPlanner = actorsPlanner ?? throw new ArgumentNullException(nameof(actorsPlanner));
        _coinCountRandomizer = coinCountRandomizer ?? throw new ArgumentNullException(nameof(coinCountRandomizer));
        _passedWavesNumber = 0;

        CurrentWaveNumber = 0;
        HealthCoefficient = actorsHealthCoefficient;
        Index = index;
    }

    public float HealthCoefficient { get; }
    public int Index { get; }
    public int CurrentWaveNumber { get; private set; }

    public int CountCoinsForWin => _coinCountRandomizer.GetCountCoinsForWave(Index);
    public int CountCoinsForWaves => _coinCountRandomizer.GetCountCoinsForWave(Index) * _passedWavesNumber;
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
