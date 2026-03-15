public interface ILevelFactory
{
    public const float MinActorsHealthCoefficientByLevel = 0.3f;

    public int LevelsCount { get; }

    public ILevel Create(int indexLevel);
}
