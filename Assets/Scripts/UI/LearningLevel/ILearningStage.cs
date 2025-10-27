public interface ILearningStage : IWindow
{
    public int NumberStages { get; }
    public int CurrentStage { get; }
    public int WaveNumber { get; }
    public bool IsActive { get; }

    public void Initialize();

    public void HandleСlick();
}
