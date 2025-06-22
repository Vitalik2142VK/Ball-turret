using System;

public class RewardIssuer : IRewardIssuer
{
    private const float AdditionalRewardFirstPass = 0.5f;

    private IPlayer _player;
    private ILevel _level;
    private IWinState _winState;
    private int _reward;
    private int _bonusReward;
    private int _maxReward;

    public RewardIssuer(IPlayer player, ILevel level, IWinState winState)
    {
        _player = player ?? throw new ArgumentNullException(nameof(player));
        _level = level ?? throw new ArgumentNullException(nameof(level));
        _winState = winState ?? throw new ArgumentNullException(nameof(winState));
        _reward = -1;
        _bonusReward = -1;
        _maxReward = -1;
        IsRewardIssued = false;
    }

    public bool IsRewardIssued { get; private set; }

    private bool IsFirstPass => _level.Index == _player.AchievedLevelIndex;

    public void AwardReward()
    {
        if (IsRewardIssued)
            throw new InvalidOperationException("Reward has already been issued");

        _player.Wallet.AddCoins(_reward);

        IsRewardIssued = true;

        if (IsFirstPass && _winState.IsWin)
            _player.IncreaseAchievedLevel();
    }

    public void ApplyMaxReward()
    {
        _reward = _maxReward;
    }

    public int GetReward()
    {
        if (_reward > 0)
            return _reward;

        CalculateRewards();

        return _reward;
    }

    public int GetBonusReward()
    {
        if (_bonusReward > 0)
            return _bonusReward;

        CalculateRewards();

        return _bonusReward;
    }

    private void CalculateRewards()
    {
        int rewardFirstPass = 0;

        if (_winState.IsWin)
            _reward = _level.CountCoinsWin;
        else
            _reward = _level.CountCoinsDefeat;

        _bonusReward = _reward;

        if (IsFirstPass && _winState.IsWin)
            rewardFirstPass = (int)(AdditionalRewardFirstPass * _reward);

        _reward += rewardFirstPass;
        _maxReward = _reward + _bonusReward;
    }
}
