using CannonTurret.Coin.Wallets;
using CannonTurret.LevelSystem;
using CannonTurret.PlayerSystem;
using CannonTurret.SDK.Shops;
using System;

namespace CannonTurret.Coin.Rewards
{
    public class RewardIssuer : IRewardIssuer
    {
        private const float AdditionalReward = 0.5f;

        private readonly ICoinAdder CoinAdder;
        private readonly IPlayer Player;
        private readonly ISelectedLevel Level;

        private int _reward;
        private int _bonusReward;
        private bool _isRewardIssued;

        public RewardIssuer(ICoinAdder coinAdder, IPlayer player, ISelectedLevel level)
        {
            CoinAdder = coinAdder ?? throw new ArgumentNullException(nameof(coinAdder));
            Player = player ?? throw new ArgumentNullException(nameof(player));
            Level = level ?? throw new ArgumentNullException(nameof(level));
            _reward = 0;
            _bonusReward = 0;
            _isRewardIssued = false;
        }

        public int Reward => _reward;
        public int MaxReward => _reward + _bonusReward;

        private bool IsFirstPass => Level.Index == Player.AchievedLevelIndex;

        public void PayReward() => PayReward(_reward);

        public void PayMaxReward() => PayReward(_reward + _bonusReward);

        public void CalculateRevard()
        {
            _reward = Level.CountCoinsForWaves;

            if (Level.IsFinished)
                _reward += Level.CountCoinsForWin;

            _bonusReward = _reward;
            CoinAdder.SetCoinsAdsView(_bonusReward);

            CalculateAddReward();
        }

        private void PayReward(int reward)
        {
            if (_isRewardIssued)
                throw new InvalidOperationException("Reward has already been issued");

            if (IsFirstPass && Level.IsFinished)
                Player.IncreaseAchievedLevel();

            CoinAdder.AddCoins(reward);

            _isRewardIssued = true;
        }

        private void CalculateAddReward()
        {
            int addedRevard = 0;

            if (IsFirstPass && Level.IsFinished)
                addedRevard = (int)(AdditionalReward * _reward);

            if (Player.PurchasesStorage.TryGetPurchase(out IPlayerPurchase purchase, PurchasesTypes.DisableAds))
                if (purchase.IsPurchased)
                    addedRevard = (int)(AdditionalReward * MaxReward);

            _reward += addedRevard;
        }
    }
}