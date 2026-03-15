namespace CannonTurret.LevelSystem
{
    public interface ILevelWaveData
    {
        public int CurrentWaveNumber { get; }
        public int WavesCount { get; }
        public bool AreWavesOver { get; }
    }
}