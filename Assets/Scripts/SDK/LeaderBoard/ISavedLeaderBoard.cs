public interface ISavedLeaderBoard
{
    public int MaxAchievedWave { get; }

    public void SaveNextAchievedWave();
}