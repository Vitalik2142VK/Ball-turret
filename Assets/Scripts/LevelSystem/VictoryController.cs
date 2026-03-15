using System;

public class VictoryController : IVictoryController
{
    private IEnemiesWinPlayer _enemiesWin;
    private IShooterView _shooterView;
    private IWinStatus _winStatus;

    public VictoryController(IEnemiesWinPlayer enemiesWin, IShooterView shooterView, IWinStatus winStatus)
    {
        _enemiesWin = enemiesWin ?? throw new ArgumentNullException(nameof(enemiesWin));
        _shooterView = shooterView ?? throw new ArgumentNullException(nameof(shooterView));
        _winStatus = winStatus ?? throw new ArgumentNullException(nameof(winStatus));
    }

    public void PlayVictory()
    {
        if (_winStatus.IsWin)
            _shooterView.PlayWin();
        else
            _enemiesWin.WinAll();
    }
}