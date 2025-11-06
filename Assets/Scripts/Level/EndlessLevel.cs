using System;

public class EndlessLevel : ILevel
{
    public const int IndexLevel = 0;

    private const float DefaultCoefficient = 1f;
    private const float MinHealthMultiplierPerWave = 0.1f;
    private const float ReducingCoefficientCoins = 1.25f;

    private ILevel _endlesslevel;
    private ISavedLeaderBoard _savedLeaderBoard;
    private float _healthMultiplierPerWave;
    private float _countCoinsMultiplier;
    private int _waveNumberReward;

    public EndlessLevel(ILevel endlesslevel, ISavedLeaderBoard savedLeaderBoard, float healthMultiplierPerWave)
    {
        if (healthMultiplierPerWave < MinHealthMultiplierPerWave)
            throw new ArgumentOutOfRangeException($"{nameof(healthMultiplierPerWave)} cannot be less than {MinHealthMultiplierPerWave}");

        _endlesslevel = endlesslevel ?? throw new ArgumentNullException(nameof(endlesslevel));
        _savedLeaderBoard = savedLeaderBoard ?? throw new ArgumentNullException(nameof(savedLeaderBoard));
        _healthMultiplierPerWave = healthMultiplierPerWave;
        _countCoinsMultiplier = DefaultCoefficient + healthMultiplierPerWave * ReducingCoefficientCoins;
        _waveNumberReward = WaveRepository.WaveDivider;

        CountCoinsForWin = 0;
    }

    public int CountCoinsForWin { get; private set; }

    public int Index => IndexLevel;
    public bool AreWavesOver => _endlesslevel.AreWavesOver;
    public int CurrentWaveNumber => _endlesslevel.CurrentWaveNumber;
    public float HealthCoefficient => DefaultCoefficient + _healthMultiplierPerWave * CurrentWaveNumber;
    public int CountCoinsForWaves => (int)((_endlesslevel.CountCoinsForWaves + CountCoinsForWin) * _countCoinsMultiplier);

    public bool TryGetNextWaveActorsPlanner(out IWaveActorsPlanner waveActorsPlanner)
    {
        if (CurrentWaveNumber != 0 && CurrentWaveNumber % _waveNumberReward == 0)
            CountCoinsForWin += (int)(_endlesslevel.CountCoinsForWin * _countCoinsMultiplier);

        if (CurrentWaveNumber > _savedLeaderBoard.MaxAchievedWave)
            _savedLeaderBoard.SaveNextAchievedWave();

        return _endlesslevel.TryGetNextWaveActorsPlanner(out waveActorsPlanner);
    }
}
