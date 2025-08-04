public interface ILevel
{
    public const int LearningLevelIndex = -1;

    public float ActorsHealthCoefficient { get; }
    public int CurrentWaveNumber { get; }
    public int CountCoinsForWin { get; }
    public int CountCoinsForWaves { get; }
    public int Index {  get; }
    public bool AreWavesOver { get; }

    public bool TryGetNextWaveActorsPlanner(out IWaveActorsPlanner waveActorsPlanner);
}
