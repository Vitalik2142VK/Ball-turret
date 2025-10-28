public interface ILearningUI
{
    public int WaveNumberStage { get; }
    public bool IsProcess { get; }
    public bool IsFinished { get; }

    public void ShowLearning(int waveNumber);
}