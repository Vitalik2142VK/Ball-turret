using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Level/Selected level", fileName = "SelectedLevel", order = 51)]
    public class SelectedLevel : ScriptableObject, ILevel
    {
        private ILevel _level;

        public ILevelActorsPlanner ActorsPlanner => _level.ActorsPlanner;
        public float ActorsHealthCoefficient => _level.ActorsHealthCoefficient;
        public int CountCoinsWin => _level.CountCoinsWin;
        public int CountCoinsDefeat => _level.CountCoinsDefeat;
        public int Index => _level.Index;

        public void SetLevel(ILevel level)
        {
            _level = level ?? throw new System.ArgumentNullException(nameof(level));
        }
    }
}
