using System;

public class EndlessLevel : ILevel
{
    public const int IndexLevel = 0;

    private const float DefaultCoefficient = 1f;
    private const float MinHealthMultiplierPerWave = 0.1f;

    private ILevel _endlesslevel;
    private float _healthMultiplierPerWave;
    private int _waveNumberReward;

    public EndlessLevel(ILevel endlesslevel, float healthMultiplierPerWave = MinHealthMultiplierPerWave, int waveNumberReward = 1)
    {
        if (healthMultiplierPerWave < MinHealthMultiplierPerWave)
            throw new ArgumentOutOfRangeException($"{nameof(healthMultiplierPerWave)} cannot be less than {MinHealthMultiplierPerWave}");

        if (waveNumberReward <= 0)
            throw new ArgumentOutOfRangeException(nameof(waveNumberReward));

        _endlesslevel = endlesslevel ?? throw new ArgumentNullException(nameof(endlesslevel));
        _healthMultiplierPerWave = healthMultiplierPerWave;
        _waveNumberReward = waveNumberReward;

        CountCoinsForWin = 0;
        _waveNumberReward = waveNumberReward;
    }

    public int CountCoinsForWin { get; private set; }

    public int Index => IndexLevel;
    public bool AreWavesOver => _endlesslevel.AreWavesOver;
    public int CurrentWaveNumber => _endlesslevel.CurrentWaveNumber;
    public float HealthCoefficient => DefaultCoefficient + _healthMultiplierPerWave * CurrentWaveNumber;
    public int CountCoinsForWaves => _endlesslevel.CountCoinsForWaves + CountCoinsForWin;

    public bool TryGetNextWaveActorsPlanner(out IWaveActorsPlanner waveActorsPlanner)
    {
        if (CurrentWaveNumber != 0 && CurrentWaveNumber % _waveNumberReward == 0)
            CountCoinsForWin += _endlesslevel.CountCoinsForWin;

        return _endlesslevel.TryGetNextWaveActorsPlanner(out waveActorsPlanner);
    }
}
