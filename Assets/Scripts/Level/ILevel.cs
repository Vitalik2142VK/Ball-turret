public interface ILevel : IActorHealthModifier, ILevelWaveData
{
    public int CountCoinsForWin { get; }
    public int CountCoinsForWaves { get; }
    public int Index {  get; }

    public bool TryGetNextWaveActorsPlanner(out IWaveActorsPlanner waveActorsPlanner);

    public ILevel Clone();
}
