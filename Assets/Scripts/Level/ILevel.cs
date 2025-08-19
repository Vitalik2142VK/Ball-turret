public interface ILevel : IActorHealthModifier
{
    public int CurrentWaveNumber { get; }
    public int CountCoinsForWin { get; }
    public int CountCoinsForWaves { get; }
    public int Index {  get; }
    public bool AreWavesOver { get; }

    public bool TryGetNextWaveActorsPlanner(out IWaveActorsPlanner waveActorsPlanner);
}
