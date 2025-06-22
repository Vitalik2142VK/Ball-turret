public interface ICoinCountRandomizer
{
    public int CountCoinsForRewardAd {  get; }

    public int GetCountCoinsWin(int indexLevel);

    public int GetCountCoinsDefeat(int indexLevel);
}
