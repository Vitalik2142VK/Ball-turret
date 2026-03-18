using CannonTurret.LevelSystem;
using System;
using UnityEngine;

namespace CannonTurret.Scriptable.Level
{
    [CreateAssetMenu(menuName = "Level/Level Factory", fileName = "LevelFactory", order = 51)]
    public class LevelFactory : ScriptableObject, ILevelFactory
    {
        private const float DefaultCoefficient = 1f;

        [SerializeField] private ActorsPlannerStore _actorsPlannerStore;

        private ICoinCountRandomizer _coinCountRandomizer;
        private float _actorsHealthCoefficientByLevel;

        public int LevelsCount => _actorsPlannerStore.LevelsCount;

        private void OnValidate()
        {
            if (_actorsPlannerStore == null)
                throw new NullReferenceException(nameof(_actorsPlannerStore));
        }

        public void Initioalize(ICoinCountRandomizer coinCountRandomizer, float actorsHealthCoefficientByLevel)
        {
            if (actorsHealthCoefficientByLevel < ILevelFactory.MinActorsHealthCoefficientByLevel)
                throw new ArgumentOutOfRangeException(nameof(actorsHealthCoefficientByLevel));

            _coinCountRandomizer = coinCountRandomizer ?? throw new ArgumentNullException(nameof(coinCountRandomizer));
            _actorsHealthCoefficientByLevel = actorsHealthCoefficientByLevel;
            _actorsPlannerStore.Initialize();
        }

        public ILevel Create(int indexLevel)
        {
            if (_actorsPlannerStore.HasIndex(indexLevel) == false)
                throw new ArgumentOutOfRangeException($"The index cannot be less than 0, greater than or equal to 1 {LevelsCount}");

            var levelActorsPlanner = _actorsPlannerStore.GetLevelActorsPlanner(indexLevel);
            float actorsHealthCoefficient = CalculateActorsHealthCoefficient(indexLevel);

            return new LevelSystem.Level(levelActorsPlanner, _coinCountRandomizer, actorsHealthCoefficient, indexLevel);
        }

        private float CalculateActorsHealthCoefficient(int indexLevel)
        {
            return DefaultCoefficient + _actorsHealthCoefficientByLevel * indexLevel;
        }
    }
}
