using System;

public class LevelFactory : ILevelFactory
{
    public const float MinActorsHealthCoefficientByLevel = 0.3f;

    private const float DefaultCoefficient = 1f;

    private IActorsPlannerStore _actorsPlannerStore;
    private ICoinCountRandomizer _coinCountRandomizer;
    private float _actorsHealthCoefficientByLevel;

    public LevelFactory(IActorsPlannerStore actorsPlannerStore, ICoinCountRandomizer coinCountRandomizer, float actorsHealthCoefficientByLevel)
    {
        if (actorsHealthCoefficientByLevel < MinActorsHealthCoefficientByLevel)
            throw new ArgumentOutOfRangeException(nameof(actorsHealthCoefficientByLevel));

        _actorsPlannerStore = actorsPlannerStore ?? throw new ArgumentNullException(nameof(actorsPlannerStore));
        _coinCountRandomizer = coinCountRandomizer ?? throw new ArgumentNullException(nameof(coinCountRandomizer));
        _actorsHealthCoefficientByLevel = actorsHealthCoefficientByLevel;

        LevelsCount = _actorsPlannerStore.LevelsCount;
    }

    public int LevelsCount { get; }

    public ILevel Create(int indexLevel)
    {
        if (_actorsPlannerStore.HasIndex(indexLevel))
            throw new ArgumentOutOfRangeException($"The index cannot be less than 0, greater than or equal to 1 {LevelsCount}");

        var levelActorsPlanner = _actorsPlannerStore.GetLevelActorsPlanner(indexLevel);
        float actorsHealthCoefficient = CalculateActorsHealthCoefficient(indexLevel);

        return new Level(levelActorsPlanner, _coinCountRandomizer, actorsHealthCoefficient, indexLevel);
    }

    private float CalculateActorsHealthCoefficient(int indexLevel)
    {
        return DefaultCoefficient + (_actorsHealthCoefficientByLevel * indexLevel);
    }
}