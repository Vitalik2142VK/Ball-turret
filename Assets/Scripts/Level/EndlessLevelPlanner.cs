using System;
using UnityEngine;

public class EndlessLevelPlanner : MonoBehaviour, ILevelActorsPlanner
{
    [SerializeField] private WaveRepository _wavesWithBonuses;
    [SerializeField] private WaveRepository[] _waveRepositories;
    [SerializeField, Min(10)] private int _bonusWavesLimit;

    private void OnValidate()
    {
        if (_wavesWithBonuses == null)
            throw new NullReferenceException(nameof(_wavesWithBonuses));

        if (_waveRepositories == null || _waveRepositories.Length == 0)
            throw new InvalidOperationException(nameof(_waveRepositories));

        foreach (var waveRepository in _waveRepositories)
            if (waveRepository == null)
                throw new NullReferenceException($"{_waveRepositories} has null elements");
    }

    public int CountWaves => int.MaxValue;

    public void Initialize()
    {
        System.Random random = new System.Random();

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
