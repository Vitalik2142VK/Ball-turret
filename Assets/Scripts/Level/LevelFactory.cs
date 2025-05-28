using System;
using System.Collections.Generic;

public class LevelFactory : ILevelFactory
{
    private const float DefaultCoefficient = 1f;
    private const float MinActorsHealthCoefficientByLevel = 0.3f;

    private List<ILevelActorsPlanner> _actorsPlanners;
    private ICoinCountRandomizer _coinCountRandomizer;
    private float _actorsHealthCoefficientByLevel;

    public LevelFactory(IEnumerable<ILevelActorsPlanner> actorsPlanners, float actorsHealthCoefficientByLevel = MinActorsHealthCoefficientByLevel)
    {
        if (actorsHealthCoefficientByLevel < MinActorsHealthCoefficientByLevel)
            throw new ArgumentOutOfRangeException(nameof(actorsHealthCoefficientByLevel));

        if (actorsPlanners == null)
            throw new ArgumentNullException(nameof(actorsPlanners));

        _actorsPlanners = new List<ILevelActorsPlanner>(actorsPlanners);

        if (_actorsPlanners.Count == 0)
            throw new ArgumentOutOfRangeException(nameof(actorsPlanners));

        _actorsHealthCoefficientByLevel = actorsHealthCoefficientByLevel;

        _coinCountRandomizer = new CoinCountRandomizer();
    }

    public int LevelsCount => _actorsPlanners.Count;

    public ILevel Create(int indexLevel)
    {
        if (indexLevel < 0 || indexLevel >= LevelsCount)
            throw new ArgumentOutOfRangeException($"The index cannot be less than 0, greater than or equal to 1 {LevelsCount}");

        ILevelActorsPlanner levelActorsPlanner = _actorsPlanners[indexLevel];
        float actorsHealthCoefficient = CalculateActorsHealthCoefficient(indexLevel);
        int countCoinsWin = _coinCountRandomizer.GetCountCoinsWin(indexLevel);
        int countCoinsDefeat = _coinCountRandomizer.GetCountCoinsDefeat(indexLevel);

        return new Level(levelActorsPlanner, actorsHealthCoefficient, countCoinsWin, countCoinsDefeat, indexLevel);
    }

    private float CalculateActorsHealthCoefficient(int indexLevel)
    {
        return DefaultCoefficient + (_actorsHealthCoefficientByLevel * indexLevel);
    }
}
