using System;
using UnityEngine;

public class WaveRepositories : MonoBehaviour, IWaveRepository
{
    [SerializeField] private WaveRepository _wavesWithBonuses;
    [SerializeField] private WaveRepository[] _waveRepositories;
    [SerializeField, Min(10)] private int _bonusWavesLimit;

    private void Initialize()
    {
        Random random = new Random();

        _wavesWithBonuses.Initialize(random);

        foreach (var waveRepository in _waveRepositories)
            waveRepository.Initialize(random);
    }

    public IWaveActorsPlanner GetWaveActorsPlanner(int waveNumber)
    {
        int modifiedWaveNumber = waveNumber % WaveRepository.WaveDivider;

        if (waveNumber < _bonusWavesLimit)
            if (_wavesWithBonuses.HasWaveNumber(modifiedWaveNumber))
                return _wavesWithBonuses.GetWaveActorsPlanner(modifiedWaveNumber);
        
        foreach (var waveRepository in _waveRepositories)
            if (waveRepository.HasWaveNumber(modifiedWaveNumber))
                return waveRepository.GetWaveActorsPlanner(modifiedWaveNumber);

        throw new InvalidOperationException("There is no suitable repository in with this wave number");
    }
}
