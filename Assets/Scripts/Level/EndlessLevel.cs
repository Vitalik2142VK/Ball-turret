using System;

public class EndlessLevel : ILevel
{
    private const float DefaultCoefficient = 1f;
    private const float MinHealthMultiplierPerWave = 0.1f;

    private IWaveRepository _waveRepository;
    private ICoinCountRandomizer _coinCountRandomizer;
    private float _healthMultiplierPerWave;

    public EndlessLevel(IWaveRepository waveRepository, ICoinCountRandomizer coinCountRandomizer, float healthMultiplierPerWave = MinHealthMultiplierPerWave)
    {
        if (healthMultiplierPerWave < MinHealthMultiplierPerWave)
            throw new ArgumentOutOfRangeException($"{nameof(healthMultiplierPerWave)} cannot be less than {MinHealthMultiplierPerWave}");

        _waveRepository = waveRepository ?? throw new ArgumentNullException(nameof(waveRepository));
        _coinCountRandomizer = coinCountRandomizer ?? throw new ArgumentNullException(nameof(coinCountRandomizer));
        _healthMultiplierPerWave = healthMultiplierPerWave;

        CurrentWaveNumber = 0;
        CountCoinsForWin = 0;
        Index = 0;
        AreWavesOver = false;
    }

    public int Index { get; }
    public bool AreWavesOver { get; }
    public int CountCoinsForWin { get; }
    public int CurrentWaveNumber { get; private set; }

    public float ActorsHealthCoefficient => DefaultCoefficient + _healthMultiplierPerWave * CurrentWaveNumber;
    public int CountCoinsForWaves => _coinCountRandomizer.GetCountCoinsForWave(Index) * CurrentWaveNumber;

    public bool TryGetNextWaveActorsPlanner(out IWaveActorsPlanner waveActorsPlanner)
    {
        waveActorsPlanner = _waveRepository.GetWaveActorsPlanner(++CurrentWaveNumber);

        return true;
    }
}
