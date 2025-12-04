using System;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Level/Selected level", fileName = "SelectedLevel", order = 51)]
    public class SelectedLevel : ScriptableObject, ISelectedLevel
    {
        private ILevel _level;
        private IViewWavesCounter _viewWavesCounter;

        public float HealthCoefficient => _level.HealthCoefficient;
        public int Index => _level.Index;
        public int CurrentWaveNumber => _level.CurrentWaveNumber;
        public int WavesCount => _level.WavesCount;
        public int CountCoinsForWin => _level.CountCoinsForWin;
        public int CountCoinsForWaves => _level.CountCoinsForWaves;
        public bool AreWavesOver => _level.AreWavesOver;
        public bool HasLevel => _level != null;

        public bool IsFinished { get; private set; }


        public ILevel Clone() => _level.Clone();

        public void SetLevel(ILevel level)
        {
            _level = level ?? throw new ArgumentNullException(nameof(level));
            IsFinished = false;
        }

        public void SetViewWavesCounter(IViewWavesCounter viewWavesCounter)
        {
            _viewWavesCounter = viewWavesCounter ?? throw new ArgumentNullException(nameof(viewWavesCounter));
            _viewWavesCounter.Initialize(_level);
        }

        public bool TryGetNextWaveActorsPlanner(out IWaveActorsPlanner waveActorsPlanner)
        {
            bool hasWave = _level.TryGetNextWaveActorsPlanner(out waveActorsPlanner);

            if (hasWave == false)
                IsFinished = true;
            else
                _viewWavesCounter.UpdateData();

            return hasWave;
        }
    }
}
