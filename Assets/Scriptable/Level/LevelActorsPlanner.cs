using System;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Level/Level Actors Planner", fileName = "LevelActorsPlanner", order = 51)]
    public class LevelActorsPlanner : ScriptableObject, ILevelActorsPlanner
    {
        [SerializeField] private WaveActorsPlanner[] _waveis;

        private void OnValidate()
        {
            if (_waveis == null || _waveis.Length == 0)
                throw new NullReferenceException(nameof(_waveis));

            foreach (var wave in _waveis)
                if (wave == null)
                    throw new NullReferenceException($"{_waveis} has null elements");
        }

        public int CountWaves => _waveis.Length;

        public IWaveActorsPlanner GetWaveActorsPlanner(int waveNumber)
        {
            int index = waveNumber - 1;

            if (index < 0 || index >= CountWaves)
                throw new System.ArgumentOutOfRangeException(nameof(waveNumber));

            return _waveis[index];
        }
    }
}

