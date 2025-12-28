using System;

public class RewardIssuer : IRewardIssuer
{
    private const float AdditionalReward = 0.5f;

    private ICoinAdder _coinAdder;
    private IPlayer _player;
    private ISelectedLevel _level;
    private int _reward;
    private int _bonusReward;
    public bool _isRewardIssued;

    public RewardIssuer(ICoinAdder coinAdder, IPlayer player, ISelectedLevel level)
    {
        _coinAdder = coinAdder ?? throw new ArgumentNullException(nameof(coinAdder));
        _player = player ?? throw new ArgumentNullException(nameof(player));
        _level = level ?? throw new ArgumentNullException(nameof(level));
        _reward = 0;
        _bonusReward = 0;
        _isRewardIssued = false;
    }

    public int Reward => _reward;
    public int MaxReward => _reward + _bonusReward;

    private bool IsFirstPass => _level.Index == _player.AchievedLevelIndex;

    public void PayReward() => PayReward(_reward);

    public void PayMaxReward() => PayReward(_reward + _bonusReward);

    public void CalculateRevard()
    {
        _reward = _level.CountCoinsForWaves;

        if (_level.IsFinished)
            _reward += _level.CountCoinsForWin;

        _bonusReward = _reward;
        _coinAdder.SetCoinsAdsView(_bonusReward);

        CalculateAddReward();
    }

    private void PayReward(int reward)
    {
        if (_isRewardIssued)
            throw new InvalidOperationException("Reward has already been issued");

        if (IsFirstPass && _level.IsFinished)
            _player.IncreaseAchievedLevel();

        _coinAdder.AddCoins(reward);

        _isRewardIssued = true;
    }

    private void CalculateAddReward()
    {
        int addedRevard = 0;

        if (IsFirstPass && _level.IsFinished)
            addedRevard = (int)(AdditionalReward * _reward);

        if (_player.PurchasesStorage.TryGetPurchase(out IPlayerPurchase purchase, PurchasesTypes.DisableAds))
            if (purchase.IsPurchased)
                addedRevard = (int)(AdditionalReward * MaxReward);

        _reward += addedRevard;
    }
}
