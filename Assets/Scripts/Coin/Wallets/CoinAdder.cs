using CannonTurret.PlayerSystem;
using CannonTurret.SDK.Ads;
using System;

namespace CannonTurret.Coin.Wallets
{
    public class CoinAdder : ICoinAdder
    {
        private readonly IPlayerSaver PlayerSaver;
        private readonly IWallet Wallet;
        private readonly IAdsViewer AdsViewer;

        public CoinAdder(IPlayerSaver playerSaver, IWallet wallet, IAdsViewer adsViewer)
        {
            PlayerSaver = playerSaver ?? throw new ArgumentNullException(nameof(playerSaver));
            Wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            AdsViewer = adsViewer ?? throw new ArgumentNullException(nameof(adsViewer));
            CoinsCountAdsView = 0;

            AdsViewer.RewardAdShowed += OnAddCoins;
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

            Wallet.AddCoins(coinsCount);
            PlayerSaver.Save();
        }

        public void Disable()
        {
            AdsViewer.RewardAdShowed -= OnAddCoins;
        }

        private void OnAddCoins(string rewardId)
        {
            if (rewardId != RewardTypes.AddCoin)
                return;

            AddCoins(CoinsCountAdsView);
        }
    }
}