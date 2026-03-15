using CannonTurret.Actors.Spawn;
using System;
using UnityEngine;

namespace CannonTurret.Scriptable.Level
{
    [CreateAssetMenu(menuName = "Level/Level Actors Planner", fileName = "LevelActorsPlanner", order = 51)]
    public class LevelActorsPlanner : ScriptableObject, ILevelActorsPlanner
    {
        [SerializeField] private WaveActorsPlanner[] _waves;

        private void OnValidate()
        {
            if (_waves == null || _waves.Length == 0)
                throw new NullReferenceException(nameof(_waves));

            foreach (var wave in _waves)
                if (wave == null)
                    throw new NullReferenceException($"{_waves} has null elements");
        }

        public int WavesCount => _waves.Length;

        public IWaveActorsPlanner GetWaveActorsPlanner(int waveNumber)
        {
            int index = waveNumber - 1;

            if (index < 0 || index >= WavesCount)
                throw new ArgumentOutOfRangeException(nameof(waveNumber));

            return _waves[index];
        }
    }
}
