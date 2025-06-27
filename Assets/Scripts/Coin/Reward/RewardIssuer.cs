using System;

public class RewardIssuer : IRewardIssuer
{
    private const float AdditionalRewardFirstPass = 0.5f;
    private int _initialRewardValue = -1;
    private int _paidRewardValue = 0;

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
        _reward = _initialRewardValue;
        _bonusReward = _initialRewardValue;
    }

    public bool IsRewardIssued => _reward == _paidRewardValue;
    public bool IsBonusRewardIssued => _bonusReward == _paidRewardValue;
    private bool IsFirstPass => _level.Index == _player.AchievedLevelIndex;


    public void AwardReward()
    {
        if (IsRewardIssued)
            throw new InvalidOperationException("Reward has already been issued");

        _player.Wallet.AddCoins(_reward);
        _reward = _paidRewardValue;

        if (IsFirstPass && _winState.IsWin)
            _player.IncreaseAchievedLevel();

        _playerSaver.Save();
    }

    public void AwarBonusReward()
    {
        if (IsBonusRewardIssued)
            throw new InvalidOperationException("Bonys reward has already been issued");

        _player.Wallet.AddCoins(_bonusReward);
        _bonusReward = _paidRewardValue;
        _playerSaver.Save();
    }

    public int GetReward()
    {
        if (_reward >= _paidRewardValue)
            return _reward;

        CalculateRewards();

        return _reward;
    }

    public int GetBonusReward()
    {
        if (_bonusReward >= _paidRewardValue)
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
    }
}
