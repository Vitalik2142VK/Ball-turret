using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Level/Level Actors Planner", fileName = "LevelActorsPlanner", order = 51)]
    public class LevelActorsPlanner : ScriptableObject, ILevelActorsPlanner
    {
        [SerializeField] private WaveActorsPlanner[] _waveis;

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

