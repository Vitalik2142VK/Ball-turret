public interface ILevel
{
    public ILevelActorsPlanner ActorsPlanner { get; }
    public float ActorsHealthCoefficient { get; }
    public int CountCoinsWin { get; }
    public int CountCoinsDefeat { get; }
}
