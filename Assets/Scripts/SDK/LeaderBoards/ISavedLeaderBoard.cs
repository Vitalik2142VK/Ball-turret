namespace CannonTurret.SDK.LeaderBoards
{
    public interface ISavedLeaderBoard
    {
        public int MaxAchievedWave { get; }

        public void SaveNextAchievedWave();
    }
}