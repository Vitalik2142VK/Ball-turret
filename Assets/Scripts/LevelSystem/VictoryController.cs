using CannonTurret.Actors.Enemies;
using CannonTurret.Turrets.Shooters;
using System;

namespace CannonTurret.LevelSystem
{
    public class VictoryController : IVictoryController
    {
        private readonly IEnemiesWinPlayer EnemiesWin;
        private readonly IShooterView ShooterView;
        private readonly IWinStatus WinStatus;

        public VictoryController(IEnemiesWinPlayer enemiesWin, IShooterView shooterView, IWinStatus winStatus)
        {
            EnemiesWin = enemiesWin ?? throw new ArgumentNullException(nameof(enemiesWin));
            ShooterView = shooterView ?? throw new ArgumentNullException(nameof(shooterView));
            WinStatus = winStatus ?? throw new ArgumentNullException(nameof(winStatus));
        }

        public void PlayVictory()
        {
            if (WinStatus.IsWin)
                ShooterView.PlayWin();
            else
                EnemiesWin.WinAll();
        }
    }
}