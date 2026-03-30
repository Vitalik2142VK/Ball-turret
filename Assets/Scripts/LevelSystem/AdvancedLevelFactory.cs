using CannonTurret.Actors.Spawn;
using CannonTurret.SDK.LeaderBoards;
using System;

namespace CannonTurret.LevelSystem
{
    public class AdvancedLevelFactory : ILevelFactory
    {
        private readonly ILevelFactory _levelFactory;
        private readonly ILevelActorsPlanner _endlessLevelPlanner;
        private readonly float _actorsHealthCoefficientByLevel;
        private readonly int _achievedLevelIndex;

        public AdvancedLevelFactory(
            ILevelFactory levelFactory,
            ILevelActorsPlanner endlessLevelPlanner,
            float actorsHealthCoefficientByLevel,
            int achievedLevelIndex)
        {
            if (actorsHealthCoefficientByLevel < ILevelFactory.MinActorsHealthCoefficientByLevel)
                throw new ArgumentOutOfRangeException(nameof(actorsHealthCoefficientByLevel));

            if (achievedLevelIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(achievedLevelIndex));

            _levelFactory = levelFactory ?? throw new ArgumentNullException(nameof(levelFactory));
            _endlessLevelPlanner = endlessLevelPlanner ?? throw new ArgumentNullException(nameof(endlessLevelPlanner));
            _actorsHealthCoefficientByLevel = actorsHealthCoefficientByLevel;
            _achievedLevelIndex = achievedLevelIndex;

            LevelsCount = _levelFactory.LevelsCount + 1;
        }

        public int LevelsCount { get; }

        public ILevel Create(int indexLevel)
        {
            if (indexLevel == EndlessLevel.IndexLevel)
                return CreateEndlessLevel();

            return _levelFactory.Create(indexLevel);
        }

        private EndlessLevel CreateEndlessLevel()
        {
            Level level = new Level(_endlessLevelPlanner);
            SavedLeaderBoard savedLeaderBoard = new SavedLeaderBoard();
            float reducingCoefficient = 0.1f;
            float finishCoefficient = (float)(_achievedLevelIndex * reducingCoefficient);
            float healthMultiplierPerWave = _actorsHealthCoefficientByLevel + finishCoefficient;

            return new EndlessLevel(level, savedLeaderBoard, healthMultiplierPerWave);
        }
    }
}