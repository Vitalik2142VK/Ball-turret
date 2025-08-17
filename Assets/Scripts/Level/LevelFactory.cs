using System;
using System.Collections.Generic;

public class LevelFactory : ILevelFactory
{
    private const float DefaultCoefficient = 1f;
    private const float MinActorsHealthCoefficientByLevel = 0.3f;
    private const float CoefficientHealthPerWave = 0.5f;

    private List<ILevelActorsPlanner> _actorsPlanners;
    private ICoinCountRandomizer _coinCountRandomizer;
    private float _actorsHealthCoefficientByLevel;

    public LevelFactory(ILevelActorsPlanner endlessLevelPlanner, IEnumerable<ILevelActorsPlanner> actorsPlanners, ICoinCountRandomizer coinCountRandomizer, float actorsHealthCoefficientByLevel = MinActorsHealthCoefficientByLevel)
    {
        if (endlessLevelPlanner == null)
            throw new ArgumentNullException(nameof(endlessLevelPlanner));

        if (actorsHealthCoefficientByLevel < MinActorsHealthCoefficientByLevel)
            throw new ArgumentOutOfRangeException(nameof(actorsHealthCoefficientByLevel));

        if (actorsPlanners == null)
            throw new ArgumentNullException(nameof(actorsPlanners));

        _actorsPlanners = new List<ILevelActorsPlanner> { endlessLevelPlanner };
        _actorsPlanners.AddRange(actorsPlanners);

        _coinCountRandomizer = coinCountRandomizer ?? throw new ArgumentNullException(nameof(coinCountRandomizer));
        _actorsHealthCoefficientByLevel = actorsHealthCoefficientByLevel;
    }

    public int LevelsCount => _actorsPlanners.Count;

    public ILevel Create(int indexLevel)
    {
        if (indexLevel < 0 || indexLevel >= LevelsCount)
            throw new ArgumentOutOfRangeException($"The index cannot be less than 0, greater than or equal to 1 {LevelsCount}");

        if (indexLevel == EndlessLevel.IndexLevel)
            return CreateEndlessLevel();

        ILevelActorsPlanner levelActorsPlanner = _actorsPlanners[indexLevel];
        float actorsHealthCoefficient = CalculateActorsHealthCoefficient(indexLevel);

        return new Level(levelActorsPlanner, _coinCountRandomizer, actorsHealthCoefficient, indexLevel);
    }

    private EndlessLevel CreateEndlessLevel()
    {
        ILevelActorsPlanner endlessLevelPlanner = _actorsPlanners[EndlessLevel.IndexLevel];
        Level level = new Level(endlessLevelPlanner, _coinCountRandomizer);
        float healthMultiplierPerWave = _actorsHealthCoefficientByLevel * CoefficientHealthPerWave;

        return new EndlessLevel(level, healthMultiplierPerWave, WaveRepository.WaveDivider);
    }

    private float CalculateActorsHealthCoefficient(int indexLevel)
    {
        return DefaultCoefficient + (_actorsHealthCoefficientByLevel * indexLevel);
    }
}
