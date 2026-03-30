using CannonTurret.Actors.Enemies;
using CannonTurret.Turrets.Shooters;
using System;

namespace CannonTurret.LevelSystem
{
    public class VictoryController : IVictoryController
    {
        private readonly IEnemiesWinPlayer _enemiesWin;
        private readonly IShooterView _shooterView;
        private readonly IWinStatus _winStatus;

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
}