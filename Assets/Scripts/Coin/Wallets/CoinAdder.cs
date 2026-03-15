using System;

public class CoinAdder : ICoinAdder
{
    private IPlayerSaver _playerSaver;
    private IWallet _wallet;
    private IAdsViewer _adsViewer;

    public CoinAdder(IPlayerSaver playerSaver, IWallet wallet, IAdsViewer adsViewer)
    {
        _playerSaver = playerSaver ?? throw new ArgumentNullException(nameof(playerSaver));
        _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
        _adsViewer = adsViewer ?? throw new ArgumentNullException(nameof(adsViewer));
        CoinsCountAdsView = 0;

        _adsViewer.RewardAdShowed += OnAddCoins;
    }

    public int CoinsCountAdsView { get; private set; }

    public void SetCoinsAdsView(int coinsCount)
    {
        if (coinsCount < 0)
            throw new ArgumentOutOfRangeException(nameof(coinsCount));

        CoinsCountAdsView = coinsCount;
    }

    public void AddCoins(int coinsCount)
    {
        if (coinsCount < 0)
            throw new ArgumentOutOfRangeException(nameof(coinsCount));

        _wallet.AddCoins(coinsCount);
        _playerSaver.Save();
    }

    public void Disable()
    {
        _adsViewer.RewardAdShowed -= OnAddCoins;
    }

    private void OnAddCoins(string rewardId)
    {
        if (rewardId != RewardTypes.AddCoin)
            return;

        AddCoins(CoinsCountAdsView);
    }
}
