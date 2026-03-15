using System;
using UnityEngine;

public class WaveSelector : MonoBehaviour
{
    [SerializeField] private WaveRepository[] _waveRepositories;

    private void OnValidate()
    {
        if (_waveRepositories == null || _waveRepositories.Length == 0)
            throw new InvalidOperationException(nameof(_waveRepositories));

        foreach (var waveRepository in _waveRepositories)
            if (waveRepository == null)
                throw new NullReferenceException($"{_waveRepositories} has null elements");
    }

    public void Initialize(System.Random random)
    {
        foreach (var waveRepository in _waveRepositories)
            waveRepository.Initialize(random);
    }

    public IWaveActorsPlanner GetWaveActorsPlanner(WaveMask waveMask)
    {
        IWaveActorsPlanner waveActors;

        foreach (var waveRepository in _waveRepositories)
            if (waveRepository.TryGetWaveActorsPlanner(out waveActors, waveMask))
                return waveActors;

        throw new InvalidOperationException("There is no suitable repository in with this wave number");
    }
}
