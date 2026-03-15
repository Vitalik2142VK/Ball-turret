using System;
using UnityEngine;

public class WaveRepository : MonoBehaviour
{
    public const int WaveDivider = 5;

    [SerializeField] private Scriptable.WaveActorsPlanner[] _waves;
    [SerializeField] private WaveMask _waveMask;

    private System.Random _random;
    private string _name;

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
        _name = gameObject.name;
    }

    public bool TryGetWaveActorsPlanner(out IWaveActorsPlanner planner, WaveMask waveMask)
    {
        planner = null;

        if (HasWaveNumber(waveMask))
        {
            int indexRandom = _random.Next(_waves.Length);
            planner = _waves[indexRandom];

            return true;
        }

        return false;
    }

    private bool HasWaveNumber(WaveMask waveMask) => (_waveMask & waveMask) != 0;
}
