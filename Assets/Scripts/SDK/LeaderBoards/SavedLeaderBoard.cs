using System;
using YG;

namespace CannonTurret.SDK.LeaderBoards
{
    public class SavedLeaderBoard : ISavedLeaderBoard
    {
        private readonly SavesYG SavesYG;

        public SavedLeaderBoard()
        {
            if (YG2.isSDKEnabled == false)
                throw new InvalidOperationException("The Yandex SDK is not Enabled");

            SavesYG = YG2.saves;
        }

        public int MaxAchievedWave => SavesYG.MaxAchievedWave;

        public void SaveNextAchievedWave()
        {
            SavesYG.MaxAchievedWave++;

            YG2.SaveProgress();
            YG2.SetLeaderboard(Leaderboard.Name, SavesYG.MaxAchievedWave);
        }
    }
}