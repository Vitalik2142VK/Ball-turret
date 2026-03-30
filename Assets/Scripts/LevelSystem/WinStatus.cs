using CannonTurret.Turrets;
using System;

namespace CannonTurret.LevelSystem
{
    public class WinStatus : IWinStatus
    {
        private readonly ITurret _turret;
        private readonly ISelectedLevel _selectedLevel;

        public WinStatus(ITurret turret, ISelectedLevel selectedLevel)
        {
            _turret = turret ?? throw new ArgumentNullException(nameof(turret));
            _selectedLevel = selectedLevel ?? throw new ArgumentNullException(nameof(selectedLevel));
        }

        public bool IsWin => _turret.IsDestroyed == false && _selectedLevel.IsFinished;
    }
}