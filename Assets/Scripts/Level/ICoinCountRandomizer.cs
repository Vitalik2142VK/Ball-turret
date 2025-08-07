public interface ICoinCountRandomizer
{
    public const int DefaultCoinsWin = 1000;
    public const int DefaultCoinsWave = 200;

    public int CountCoinsForRewardAd {  get; }

    public int GetCountCoinsForWin(int indexLevel);

    public int GetCountCoinsForWave(int indexLevel);
}
