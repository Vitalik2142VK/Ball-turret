using CannonTurret.UI.Animations;
using System;
using TMPro;
using UnityEngine;

namespace CannonTurret.LevelSystem
{
    [RequireComponent(typeof(PulsingScaleAnimation))]
    public class ViewWavesCounter : MonoBehaviour, IViewWavesCounter
    {
        public const string InfiniteValue = "∞";
        public const string WaveCountFormat = "{0}/{1}";

        [SerializeField] private TextMeshProUGUI _waveCountText;

        private ILevelWaveData _levelWaveData;
        private PulsingScaleAnimation _pulsingScaleAnimation;
        private string _wavesCount;

        private void OnValidate()
        {
            if (_waveCountText == null)
                throw new NullReferenceException(nameof(_waveCountText));
        }

        private void Awake()
        {
            _pulsingScaleAnimation = GetComponent<PulsingScaleAnimation>();
        }

        public void Initialize(ILevelWaveData levelWaveData)
        {
            _levelWaveData = levelWaveData ?? throw new ArgumentNullException(nameof(_levelWaveData));
            int wavesCount = _levelWaveData.WavesCount;

            if (wavesCount == int.MaxValue)
                _wavesCount = InfiniteValue;
            else
                _wavesCount = wavesCount.ToString();

            UpdateData();
        }

        public void UpdateData()
        {
            int waveNumber = _levelWaveData.CurrentWaveNumber;

            if (_levelWaveData.AreWavesOver)
                waveNumber = _levelWaveData.WavesCount;

            _waveCountText.text = string.Format(WaveCountFormat, waveNumber, _wavesCount);
            _pulsingScaleAnimation.Play();
        }
    }
}