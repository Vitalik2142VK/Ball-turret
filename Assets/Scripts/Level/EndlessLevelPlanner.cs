using System;
using UnityEngine;

public class EndlessLevelPlanner : MonoBehaviour, ILevelActorsPlanner
{
    [SerializeField] private WaveRepository _wavesWithBonuses;
    [SerializeField] private WaveRepository[] _waveRepositories;
    [SerializeField] private WaveRepository[] _hardWaveRepositories;
    [SerializeField, Min(10)] private int _bonusWavesLimit = 10;
    [SerializeField, Min(20)] private int _standartWavesLimit = 20;

    private void OnValidate()
    {
        if (_wavesWithBonuses == null)
            throw new NullReferenceException(nameof(_wavesWithBonuses));

        if (_waveRepositories == null || _waveRepositories.Length == 0)
            throw new InvalidOperationException(nameof(_waveRepositories));

        foreach (var waveRepository in _waveRepositories)
            if (waveRepository == null)
                throw new NullReferenceException($"{_waveRepositories} has null elements");

        if (_hardWaveRepositories == null || _hardWaveRepositories.Length == 0)
            throw new InvalidOperationException(nameof(_hardWaveRepositories));

        foreach (var waveRepository in _hardWaveRepositories)
            if (waveRepository == null)
                throw new NullReferenceException($"{_hardWaveRepositories} has null elements");
    }

    public int WavesCount => int.MaxValue;

    public void Initialize()
    {
        System.Random random = new System.Random();

        _wavesWithBonuses.Initialize(random);

        foreach (var waveRepository in _waveRepositories)
            waveRepository.Initialize(random);
    }

    public IWaveActorsPlanner GetWaveActorsPlanner(int waveNumber)
    {
        if (waveNumber > _standartWavesLimit)
            return GetHardWave(waveNumber);
        else
            return GetStandartWave(waveNumber);
    }

    private IWaveActorsPlanner GetStandartWave(int waveNumber)
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

    private IWaveActorsPlanner GetHardWave(int waveNumber)
    {
        int modifiedWaveNumber = waveNumber % WaveRepository.WaveDivider;

        if (waveNumber < _bonusWavesLimit)
            if (_wavesWithBonuses.HasWaveNumber(modifiedWaveNumber))
                return _wavesWithBonuses.GetWaveActorsPlanner(modifiedWaveNumber);

        foreach (var waveRepository in _hardWaveRepositories)
            if (waveRepository.HasWaveNumber(modifiedWaveNumber))
                return waveRepository.GetWaveActorsPlanner(modifiedWaveNumber);

        throw new InvalidOperationException("There is no suitable repository in with this wave number");
    }
}
