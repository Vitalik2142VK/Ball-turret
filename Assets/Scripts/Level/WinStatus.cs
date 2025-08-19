using System;

public class WinStatus : IWinStatus
{
    private ITurret _turret;
    private ISelectedLevel _selectedLevel;

    public WinStatus(ITurret turret, ISelectedLevel selectedLevel)
    {
        _turret = turret ?? throw new ArgumentNullException(nameof(turret));
        _selectedLevel = selectedLevel ?? throw new ArgumentNullException(nameof(selectedLevel));
    }

    public bool IsWin => _turret.IsDestroyed == false && _selectedLevel.IsFinished;
}