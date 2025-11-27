using System;

public class AdvancedLevelFactory : ILevelFactory
{
    private ILevelFactory _levelFactory;
    private ILevelActorsPlanner _endlessLevelPlanner;
    private ICoinCountRandomizer _coinCountRandomizer;
    private float _actorsHealthCoefficientByLevel;
    private int _achievedLevelIndex;

    public AdvancedLevelFactory(ILevelFactory levelFactory, ILevelActorsPlanner endlessLevelPlanner, ICoinCountRandomizer coinCountRandomizer, float actorsHealthCoefficientByLevel, int achievedLevelIndex)
    {
        if (actorsHealthCoefficientByLevel < LevelFactory.MinActorsHealthCoefficientByLevel)
            throw new ArgumentOutOfRangeException(nameof(actorsHealthCoefficientByLevel));

        if (achievedLevelIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(achievedLevelIndex));

        _levelFactory = levelFactory ?? throw new ArgumentNullException(nameof(levelFactory));
        _endlessLevelPlanner = endlessLevelPlanner ?? throw new ArgumentNullException(nameof(endlessLevelPlanner));
        _coinCountRandomizer = coinCountRandomizer ?? throw new ArgumentNullException(nameof(coinCountRandomizer));
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
        Level level = new Level(_endlessLevelPlanner, _coinCountRandomizer);
        SavedLeaderBoard savedLeaderBoard = new SavedLeaderBoard();
        float reducingCoefficient = 0.1f;
        float healthMultiplierPerWave = _actorsHealthCoefficientByLevel + (float)(_achievedLevelIndex * reducingCoefficient);

        return new EndlessLevel(level, savedLeaderBoard, healthMultiplierPerWave);
    }
}
