using System;

public class RewardIssuer : IRewardIssuer
{
    private const float AdditionalRewardFirstPass = 0.5f;
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

    public void AwardReward()
    {
        if (IsRewardIssued)
            throw new InvalidOperationException("Reward has already been issued");

        _player.Wallet.AddCoins(_reward);

        if (IsFirstPass && _winState.IsWin)
            _player.IncreaseAchievedLevel();

        _playerSaver.Save();

        IsRewardIssued = true;
    }

    public void AwarBonusReward()
    {
        if (IsBonusRewardIssued)
            throw new InvalidOperationException("Bonys reward has already been issued");

        _player.Wallet.AddCoins(_bonusReward);
        _playerSaver.Save();

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

    public int GetMaxReward()
    {
        return GetReward() + GetBonusReward();
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
