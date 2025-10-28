using System;

public class RewardIssuer : IRewardIssuer
{
    private const float AdditionalReward = 0.5f;
    private const int InitialRewardValue = -1;

    private ICoinAdder _coinAdder;
    private IPlayer _player;
    private ISelectedLevel _level;
    private int _reward;
    private int _bonusReward;

    public RewardIssuer(ICoinAdder coinAdder, IPlayer player, ISelectedLevel level)
    {
        _coinAdder = coinAdder ?? throw new ArgumentNullException(nameof(coinAdder));
        _player = player ?? throw new ArgumentNullException(nameof(player));
        _level = level ?? throw new ArgumentNullException(nameof(level));
        _reward = InitialRewardValue;
        _bonusReward = InitialRewardValue;
        IsRewardIssued = false;
    }

    public bool IsRewardIssued { get; private set; }

    private bool IsFirstPass => _level.Index == _player.AchievedLevelIndex;

    public void PayReward() => PayReward(_reward);

    public void PayMaxReward() => PayReward(_reward + _bonusReward);

    public int GetReward()
    {
        if (_reward >= 0)
            return _reward;

        CalculateRewards();

        return _reward;
    }

    public int GetMaxReward() => GetReward() + _bonusReward;

    private void PayReward(int reward)
    {
        if (IsRewardIssued)
            throw new InvalidOperationException("Reward has already been issued");

        if (IsFirstPass && _level.IsFinished)
            _player.IncreaseAchievedLevel();

        _coinAdder.AddCoins(reward);

        IsRewardIssued = true;
    }

    private void CalculateRewards()
    {
        _reward = _level.CountCoinsForWaves;

        if (_level.IsFinished)
            _reward += _level.CountCoinsForWin;

        _bonusReward = _reward;
        _coinAdder.SetCoinsAdsView(_bonusReward);

        CalculateAddReward();
    }

    private void CalculateAddReward()
    {
        int addedRevard = 0;

        if (IsFirstPass && _level.IsFinished)
            addedRevard = (int)(AdditionalReward * _reward);

        if (_player.PurchasesStorage.TryGetPurchase(out IPlayerPurchase purchase, PurchasesTypes.DisableAds))
            if (purchase.IsPurchased)
                addedRevard = (int)(AdditionalReward * GetMaxReward());

        _reward += addedRevard;
    }
}
