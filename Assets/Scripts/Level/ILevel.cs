public interface ILevel
{
    public float ActorsHealthCoefficient { get; }
    public int CurrentWaveNumber { get; }
    public int PassedWavesNumber { get; }
    public int CountCoinsWin { get; }
    public int CountCoinsDefeat { get; }
    public int Index {  get; }
    public bool AreWavesOver { get; }

    public bool TryGetNextWaveActorsPlanner(out IWaveActorsPlanner waveActorsPlanner);
}
