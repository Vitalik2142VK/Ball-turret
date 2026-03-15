namespace CannonTurret.LevelSystem
{
    public interface ISelectedLevel : ILevel
    {
        public bool IsFinished { get; }

        public void SetViewWavesCounter(IViewWavesCounter viewWavesCounter);
    }
}