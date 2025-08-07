using System;
using UnityEngine;

public class WaveRepository : MonoBehaviour, IWaveRepository
{
    public const int WaveDivider = 5;

    [SerializeField] private Scriptable.WaveActorsPlanner[] _waves;
    [SerializeField, Range(0, WaveDivider - 1)] private int[] _waveNumbers;

    private Random _random;

    public void Initialize(Random random)
    {
        _random = random ?? throw new ArgumentNullException(nameof(random));
    }

    public bool HasWaveNumber(int waveNumber)
    {
        if (waveNumber >= WaveDivider)
            throw new ArgumentOutOfRangeException(nameof(waveNumber));

        foreach (var number in _waveNumbers)
            if (number == waveNumber)
                return true;

        return false;
    }

    public IWaveActorsPlanner GetWaveActorsPlanner(int waveNumber)
    {
        if (HasWaveNumber(waveNumber) == false)
            throw new ArgumentOutOfRangeException(nameof(waveNumber));

        int indexRandom = _random.Next(_waves.Length);

        return _waves[indexRandom];
    }
}