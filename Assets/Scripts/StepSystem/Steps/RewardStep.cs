using System;

public class RewardStep : IStep, IEndPointStep
{
    private IEndStep _endStep;
    private IWinState _winStat;
    private IMenu _finishWindow;
    private ITurret _turret;

    public RewardStep(IWinState winStat, IMenu finishWindow, ITurret turret)
    {
        _winStat = winStat ?? throw new ArgumentNullException(nameof(winStat));
        _finishWindow = finishWindow ?? throw new ArgumentNullException(nameof(finishWindow));
        _turret = turret ?? throw new ArgumentNullException(nameof(turret));

        _turret.Destroyed += OnActionDefeat;
    }

    public void Disable()
    {
        _turret.Destroyed -= OnActionDefeat;
    }

    public void Action()
    {
        _winStat.IsWin = true;

        GiveReward();
    }

    public void SetEndStep(IEndStep endStep)
    {
        _endStep = endStep ?? throw new ArgumentNullException(nameof(endStep));
    }

    private void OnActionDefeat()
    {
        _winStat.IsWin = false;

        GiveReward();
    }

    private void GiveReward()
    {
        _finishWindow.Enable();
        _endStep.End();
    }
}