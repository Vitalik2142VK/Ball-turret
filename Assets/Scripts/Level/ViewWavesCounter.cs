using System;
using TMPro;
using UnityEngine;

public class ViewWavesCounter : MonoBehaviour, IViewWavesCounter
{
    public const string InfiniteValue = "∞";

    [SerializeField] private TextMeshProUGUI _wavesCount;
    [SerializeField] private TextMeshProUGUI _currentWaveNumber;

    private ILevelWaveData _levelWaveData;

    private void OnValidate()
    {
        if (_wavesCount == null)
            throw new NullReferenceException(nameof(_wavesCount));

        if (_currentWaveNumber == null)
            throw new NullReferenceException(nameof(_currentWaveNumber));
    }

    public void Initialize(ILevelWaveData levelWaveData)
    {
        _levelWaveData = levelWaveData ?? throw new ArgumentNullException(nameof(_levelWaveData));
        int wavesCount = _levelWaveData.WavesCount;

        if (wavesCount == int.MaxValue)
            _wavesCount.text = InfiniteValue;
        else
            _wavesCount.text = wavesCount.ToString();

        UpdateData();
    }

    public void UpdateData()
    {
        int waveNumber = _levelWaveData.CurrentWaveNumber;

        if (_levelWaveData.AreWavesOver)
            waveNumber = _levelWaveData.WavesCount;
        else
            waveNumber++;

        _currentWaveNumber.text = waveNumber.ToString();
    }
}
