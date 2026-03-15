namespace CannonTurret.LevelSystem
{
    public interface IViewWavesCounter
    {
        public void Initialize(ILevelWaveData levelWaveData);

        public void UpdateData();
    }
}