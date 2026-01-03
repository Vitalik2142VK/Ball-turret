using System;

public class LevelStatus : ILevelStatus
{
    private ITurret _turret;
    private ISelectedLevel _selectedLevel;

    public LevelStatus(ITurret turret, ISelectedLevel selectedLevel)
    {
        _turret = turret ?? throw new ArgumentNullException(nameof(turret));
        _selectedLevel = selectedLevel ?? throw new ArgumentNullException(nameof(selectedLevel));
    }

    public bool IsComplete => _selectedLevel.IsFinished;
    public bool IsLose => _turret.IsDestroyed;
}