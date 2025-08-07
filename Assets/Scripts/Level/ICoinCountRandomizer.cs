public interface ICoinCountRandomizer
{
    public int CountCoinsForRewardAd {  get; }

    public int GetCountCoinsForWin(int indexLevel);

    public int GetCountCoinsForWave(int indexLevel);
}
