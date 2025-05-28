using System;

public class RewardIssuer : IRewardIssuer
{
    private const float AdditionalRewardFirstPass = 0.5f;
    private const int DoubleReward = 2;

    private IUser _user;
    private ILevel _level;
    private IWinState _winState;
    private int _rewardMultiplier;
    private bool _rewardIssued;

    public RewardIssuer(IUser user, ILevel level, IWinState winState)
    {
        _user = user ?? throw new ArgumentNullException(nameof(user));
        _level = level ?? throw new ArgumentNullException(nameof(level));
        _winState = winState ?? throw new ArgumentNullException(nameof(winState));
        _rewardMultiplier = 1;
        _rewardIssued = false;
    }

    private bool IsFirstPass => _level.Index == _user.AchievedLevelIndex;

    public void AwardReward()
    {
        if (_rewardIssued)
            throw new InvalidOperationException("Reward has already been issued.");

        int reward = GetReward();
        _user.Wallet.AddCoins(reward);

        if (IsFirstPass && _winState.IsWin)
            _user.IncreaseAchievedLevel();
    }

    public void ApplyDoublingReward()
    {
        if (DoubleReward <= _rewardMultiplier)
            throw new InvalidOperationException($"The rewards cannot be increased by more than {DoubleReward} times.");

        _rewardMultiplier = DoubleReward;
    }

    public int GetReward()
    {
        int reward;
        int rewardFirstPass = 0;

        if (_winState.IsWin)
            reward = _level.CountCoinsWin;
        else
            reward = _level.CountCoinsDefeat;

        if (IsFirstPass)
            rewardFirstPass = (int)(AdditionalRewardFirstPass * reward);

        return reward * _rewardMultiplier + rewardFirstPass;
    }
}
