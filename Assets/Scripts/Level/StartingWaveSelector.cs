using System;
using UnityEngine;

public class StartingWaveSelector : MonoBehaviour
{
    [SerializeField] private WaveRepository _waveWithBonusesRepository;
    [SerializeField] private WaveSelector _standartWaveSelector;
    [SerializeField, Min(10)] private int _bonusWavesLimit = 10;

    private void OnValidate()
    {
        if (_waveWithBonusesRepository == null)
            throw new NullReferenceException(nameof(_waveWithBonusesRepository));

        if (_standartWaveSelector == null)
            throw new NullReferenceException(nameof(_standartWaveSelector));
    }

    public void Initialize(System.Random random)
    {
        _waveWithBonusesRepository.Initialize(random);
        _standartWaveSelector.Initialize(random);
    }

    public IWaveActorsPlanner GetWaveActorsPlanner(WaveMask waveMask, int waveNumber)
    {
        if (waveNumber < _bonusWavesLimit)
            if (_waveWithBonusesRepository.TryGetWaveActorsPlanner(out IWaveActorsPlanner waveActors, waveMask))
                return waveActors;

        return _standartWaveSelector.GetWaveActorsPlanner(waveMask);
    }
}