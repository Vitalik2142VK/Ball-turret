using CannonTurret.Actors.Spawn;
using CannonTurret.SDK.LeaderBoards;
using System;

namespace CannonTurret.LevelSystem
{
    public class AdvancedLevelFactory : ILevelFactory
    {
        private readonly ILevelFactory LevelFactory;
        private readonly ILevelActorsPlanner EndlessLevelPlanner;
        private readonly ICoinCountRandomizer CoinCountRandomizer;
        private readonly float ActorsHealthCoefficientByLevel;
        private readonly int AchievedLevelIndex;

        public AdvancedLevelFactory(ILevelFactory levelFactory, ILevelActorsPlanner endlessLevelPlanner, ICoinCountRandomizer coinCountRandomizer, float actorsHealthCoefficientByLevel, int achievedLevelIndex)
        {
            if (actorsHealthCoefficientByLevel < ILevelFactory.MinActorsHealthCoefficientByLevel)
                throw new ArgumentOutOfRangeException(nameof(actorsHealthCoefficientByLevel));

            if (achievedLevelIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(achievedLevelIndex));

            LevelFactory = levelFactory ?? throw new ArgumentNullException(nameof(levelFactory));
            EndlessLevelPlanner = endlessLevelPlanner ?? throw new ArgumentNullException(nameof(endlessLevelPlanner));
            CoinCountRandomizer = coinCountRandomizer ?? throw new ArgumentNullException(nameof(coinCountRandomizer));
            ActorsHealthCoefficientByLevel = actorsHealthCoefficientByLevel;
            AchievedLevelIndex = achievedLevelIndex;

            LevelsCount = LevelFactory.LevelsCount + 1;
        }

        public int LevelsCount { get; }

        public ILevel Create(int indexLevel)
        {
            if (indexLevel == EndlessLevel.IndexLevel)
                return CreateEndlessLevel();

            return LevelFactory.Create(indexLevel);
        }

        private EndlessLevel CreateEndlessLevel()
        {
            Level level = new Level(EndlessLevelPlanner, CoinCountRandomizer);
            SavedLeaderBoard savedLeaderBoard = new SavedLeaderBoard();
            float reducingCoefficient = 0.1f;
            float healthMultiplierPerWave = ActorsHealthCoefficientByLevel + (float)(AchievedLevelIndex * reducingCoefficient);

            return new EndlessLevel(level, savedLeaderBoard, healthMultiplierPerWave);
        }
    }
}