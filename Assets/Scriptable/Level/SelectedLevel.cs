using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Level/Selected level", fileName = "SelectedLevel", order = 51)]
    public class SelectedLevel : ScriptableObject, ISelectedLevel
    {
        private ILevel _level;

        public float HealthCoefficient => _level.HealthCoefficient;
        public int CurrentWaveNumber => _level.CurrentWaveNumber;
        public int CountCoinsForWin => _level.CountCoinsForWin;
        public int CountCoinsForWaves => _level.CountCoinsForWaves;
        public int Index => _level.Index;
        public bool AreWavesOver => _level.AreWavesOver;

        public bool IsFinished { get; private set; }

        public void Initialize(ILevel level)
        {
            _level = level ?? throw new System.ArgumentNullException(nameof(level));
            IsFinished = false;
        }

        public bool TryGetNextWaveActorsPlanner(out IWaveActorsPlanner waveActorsPlanner)
        {
            bool hasWave = _level.TryGetNextWaveActorsPlanner(out waveActorsPlanner);

            if (hasWave == false)
                IsFinished = true;

            return hasWave;
        }
    }
}
