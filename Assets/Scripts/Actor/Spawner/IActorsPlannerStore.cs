public interface IActorsPlannerStore
{
    public int LevelsCount { get; }

    public bool HasIndex(int index);

    public ILevelActorsPlanner GetLevelActorsPlanner(int index);
}
