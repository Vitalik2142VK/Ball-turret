public interface ILevelActorsPlanner
{
    public int WavesCount { get; }

    public IWaveActorsPlanner GetWaveActorsPlanner(int waveNumber);
}
