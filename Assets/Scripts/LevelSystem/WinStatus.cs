using CannonTurret.Turrets;
using System;

namespace CannonTurret.LevelSystem
{
    public class WinStatus : IWinStatus
    {
        private readonly ITurret Turret;
        private readonly ISelectedLevel SelectedLevel;

        public WinStatus(ITurret turret, ISelectedLevel selectedLevel)
        {
            Turret = turret ?? throw new ArgumentNullException(nameof(turret));
            SelectedLevel = selectedLevel ?? throw new ArgumentNullException(nameof(selectedLevel));
        }

        public bool IsWin => Turret.IsDestroyed == false && SelectedLevel.IsFinished;
    }
}