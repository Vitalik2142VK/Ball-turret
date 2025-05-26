using System;

public class RewardIssuer : IWinState, IRewardIssuer
{
    private const float AdditionalRewardFirstPass = 0.5f;
    private const int DoubleReward = 2;

    private IUser _user;
    private ILevel _level;
    private int _rewardMultiplier;
    private bool _rewardIssued;

    public RewardIssuer(IUser user, ILevel level)
    {
        _user = user ?? throw new ArgumentNullException(nameof(user));
        _level = level ?? throw new ArgumentNullException(nameof(level));
        _rewardMultiplier = 1;
        _rewardIssued = false;

        IsWin = false;
    }

    private bool IsFirstPass => _level.Index == _user.AchievedLevelIndex;

    public bool IsWin { get; set; }

    public void AwardReward()
    {
        if (_rewardIssued)
            throw new InvalidOperationException("Reward has already been issued.");

        int reward = GetReward();
        _user.Wallet.AddCoins(reward);

        if (IsFirstPass && IsWin)
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

        if (IsWin)
            reward = _level.CountCoinsWin;
        else
            reward = _level.CountCoinsDefeat;

        if (IsFirstPass)
            rewardFirstPass = (int)(AdditionalRewardFirstPass * reward);

        return reward * _rewardMultiplier + rewardFirstPass;
    }
}
