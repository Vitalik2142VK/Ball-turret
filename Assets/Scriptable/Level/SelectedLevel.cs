using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Level/Selected level", fileName = "SelectedLevel", order = 51)]
    public class SelectedLevel : ScriptableObject, ILevel
    {
        private ILevel _level;

        public float ActorsHealthCoefficient => _level.ActorsHealthCoefficient;
        public int CurrentWaveNumber => _level.CurrentWaveNumber;
        public int PassedWavesNumber => _level.PassedWavesNumber;
        public int CountCoinsWin => _level.CountCoinsWin;
        public int CountCoinsDefeat => _level.CountCoinsDefeat;
        public int Index => _level.Index;
        public bool AreWavesOver => _level.AreWavesOver;


        public void Initialize(ILevel level)
        {
            _level = level ?? throw new System.ArgumentNullException(nameof(level));
        }

        public bool TryGetNextWaveActorsPlanner(out IWaveActorsPlanner waveActorsPlanner) => _level.TryGetNextWaveActorsPlanner(out waveActorsPlanner);
    }
}
