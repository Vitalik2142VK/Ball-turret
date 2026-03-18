using CannonTurret.Actors.Spawn;
using CannonTurret.Utils;
using System;
using UnityEngine;

namespace CannonTurret.LevelSystem
{
    public class EndlessLevelPlanner : MonoBehaviour, ILevelActorsPlanner
    {
        [SerializeField] private StartingWaveSelector _startingWaveSelector;
        [SerializeField] private WaveSelector _hardWaveSelector;

        [SerializeField][Min(20)] private int _standartWavesLimit = 20;

        private int _waveMaskCount;

        private void OnValidate()
        {
            if (_startingWaveSelector == null)
                throw new NullReferenceException(nameof(_startingWaveSelector));

            if (_hardWaveSelector == null)
                throw new NullReferenceException(nameof(_hardWaveSelector));
        }

        public int WavesCount => int.MaxValue;

        public void Initialize()
        {
            _waveMaskCount = EnumHelper.GetActiveValuesCount<WaveMask>();
            System.Random random = new System.Random();

            _startingWaveSelector.Initialize(random);
            _hardWaveSelector.Initialize(random);
        }

        public IWaveActorsPlanner GetWaveActorsPlanner(int waveNumber)
        {
            int modifiedWaveNumber = waveNumber % _waveMaskCount;
            WaveMask waveMask = (WaveMask)(1 << modifiedWaveNumber);

            if (waveNumber > _standartWavesLimit)
                return _hardWaveSelector.GetWaveActorsPlanner(waveMask);
            else
                return _startingWaveSelector.GetWaveActorsPlanner(waveMask, waveNumber);
        }
    }
}