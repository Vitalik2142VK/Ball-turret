using CannonTurret.Turrets;
using System;

namespace CannonTurret.LevelSystem
{
    public class LevelStatus : ILevelStatus
    {
        private readonly ITurret Turret;
        private readonly ISelectedLevel SelectedLevel;

        public LevelStatus(ITurret turret, ISelectedLevel selectedLevel)
        {
            Turret = turret ?? throw new ArgumentNullException(nameof(turret));
            SelectedLevel = selectedLevel ?? throw new ArgumentNullException(nameof(selectedLevel));
        }

        public bool IsComplete => SelectedLevel.IsFinished;
        public bool IsLose => Turret.IsDestroyed;
    }
}