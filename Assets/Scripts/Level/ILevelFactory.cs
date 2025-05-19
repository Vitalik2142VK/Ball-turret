public interface ILevelFactory
{
    public int LevelsCount { get; }

    public ILevel Create(int indexLevel);
}
