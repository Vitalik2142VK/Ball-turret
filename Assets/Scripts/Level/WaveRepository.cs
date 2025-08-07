using System;
using UnityEngine;

public class WaveRepository : MonoBehaviour
{
    public const int WaveDivider = 5;

    [SerializeField] private Scriptable.WaveActorsPlanner[] _waves;
    [SerializeField] private WaveNumberMask _waveMask;

    private System.Random _random;

    private void OnValidate()
    {
        if (_waves == null || _waves.Length == 0)
            throw new InvalidOperationException(nameof(_waves));

        foreach (var waveRepository in _waves)
            if (waveRepository == null)
                throw new NullReferenceException($"{_waves} has null elements");
    }

    public void Initialize(System.Random random)
    {
        _random = random ?? throw new ArgumentNullException(nameof(random));
    }

    public bool HasWaveNumber(int waveNumber)
    {
        if (waveNumber >= WaveDivider)
            throw new ArgumentOutOfRangeException(nameof(waveNumber));

        WaveNumberMask waveNumberMask = (WaveNumberMask)(1 << waveNumber);

        return (_waveMask & waveNumberMask) != 0;
    }

    public IWaveActorsPlanner GetWaveActorsPlanner(int waveNumber)
    {
        if (HasWaveNumber(waveNumber) == false)
            throw new ArgumentOutOfRangeException(nameof(waveNumber));

        int indexRandom = _random.Next(_waves.Length);

        return _waves[indexRandom];
    }
}
