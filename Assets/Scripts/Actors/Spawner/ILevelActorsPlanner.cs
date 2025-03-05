public interface ILevelActorsPlanner
{
    public int CountWaves { get; }

    public IWaveActorsPlanner GetWaveActorsPlanner(int waveNumber);
}
