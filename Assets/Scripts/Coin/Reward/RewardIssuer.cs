using System;

public class RewardIssuer : IRewardIssuer
{
    private const float AdditionalReward = 0.5f;
    private const int InitialRewardValue = -1;

    private IPlayerSaver _playerSaver;
    private IPlayer _player;
    private ILevel _level;
    private IWinState _winState;
    private int _reward;
    private int _bonusReward;

    public RewardIssuer(IPlayerSaver playerSaver, IPlayer player, ILevel level, IWinState winState)
    {
        _playerSaver = playerSaver ?? throw new ArgumentNullException(nameof(playerSaver));
        _player = player ?? throw new ArgumentNullException(nameof(player));
        _level = level ?? throw new ArgumentNullException(nameof(level));
        _winState = winState ?? throw new ArgumentNullException(nameof(winState));
        _reward = InitialRewardValue;
        _bonusReward = InitialRewardValue;
        IsRewardIssued = false;
        IsBonusRewardIssued = false;
    }

    public bool IsRewardIssued { get; private set; }
    public bool IsBonusRewardIssued { get; private set; }

    private bool IsFirstPass => _level.Index == _player.AchievedLevelIndex;

    public void PayReward()
    {
        if (IsRewardIssued)
            throw new InvalidOperationException("Reward has already been issued");

        if (IsFirstPass && _winState.IsWin)
            _player.IncreaseAchievedLevel();

        _player.Wallet.AddCoins(_reward);
        _playerSaver.Save();

        IsRewardIssued = true;
    }

    public void PayBonusReward()
    {
        if (IsBonusRewardIssued)
            throw new InvalidOperationException("Bonys reward has already been issued");

        _player.Wallet.AddCoins(_bonusReward);
        _playerSaver.Save();

        IsBonusRewardIssued = true;
    }

    public void PayMaxReward()
    {
        if (IsRewardIssued || IsBonusRewardIssued)
            throw new InvalidOperationException("Reward has already been issued");

        if (IsFirstPass && _winState.IsWin)
            _player.IncreaseAchievedLevel();

        _player.Wallet.AddCoins(_reward + _bonusReward);
        _playerSaver.Save();

        IsRewardIssued = true;
        IsBonusRewardIssued = true;
    }

    public int GetReward()
    {
        if (_reward >= 0)
            return _reward;

        CalculateRewards();

        return _reward;
    }

    public int GetBonusReward()
    {
        if (_bonusReward >= 0)
            return _bonusReward;

        CalculateRewards();

        return _bonusReward;
    }

    public int GetMaxReward() => GetReward() + GetBonusReward();

    private void CalculateRewards()
    {
        if (_winState.IsWin)
            _reward = _level.CountCoinsWin;
        else
            _reward = _level.CountCoinsDefeat;

        _bonusReward = _reward;

        CalculateAddReward();
    }

    private void CalculateAddReward()
    {
        int addedRevard = 0;

        if (IsFirstPass && _winState.IsWin)
            addedRevard = (int)(AdditionalReward * _reward);

        if (_player.PurchasesStorage.TryGetPurchase(out IPurchase purchase, PurchasesTypes.DisableAds))
            if (purchase.IsConsumed == true)
                addedRevard = (int)(AdditionalReward * GetMaxReward());

        _reward += addedRevard;
    }
}
