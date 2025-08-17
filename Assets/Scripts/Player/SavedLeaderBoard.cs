using System;
using YG;

public class SavedLeaderBoard : ISavedLeaderBoard
{
    private SavesYG _savesYG;

    public SavedLeaderBoard()
    {
        if (YG2.isSDKEnabled == false)
            throw new InvalidOperationException("The Yandex SDK is not Enabled");

        _savesYG = YG2.saves;
    }

    public int MaxAchievedWave => _savesYG.MaxAchievedWave;

    public void SaveNextAchievedWave()
    {
        _savesYG.MaxAchievedWave++;

        YG2.SaveProgress();
    }
}
