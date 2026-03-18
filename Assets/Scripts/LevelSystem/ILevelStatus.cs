namespace CannonTurret.LevelSystem
{
    public interface ILevelStatus
    {
        public bool IsComplete { get; }

        public bool IsLose { get; }
    }
}