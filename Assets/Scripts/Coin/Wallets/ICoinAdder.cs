public interface ICoinAdder
{
    public int CoinsCountAdsView { get; }

    public void SetCoinsAdsView(int coinsCount);

    public void AddCoins(int coinsCount);
}