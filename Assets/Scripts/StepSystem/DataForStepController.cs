using CannonTurret.Actors;
using CannonTurret.Coin.Rewards;
using CannonTurret.LevelSystem;
using CannonTurret.PlayerSystem;
using CannonTurret.SDK.Ads;
using CannonTurret.Turrets;
using System;

namespace CannonTurret.StepSystem
{
    public class DataForStepController : IDataForStepController
    {
        public DataForStepController(
            ITurret turret,
            IAdsViewer adsViewer,
            IRewardIssuer rewardIssuer,
            IPlayerController playerController,
            IVictoryController victoryController,
            IActorsControllersAccess controllersAccess,
            ILevelStatus levelStatus)
        {
            Turret = turret ?? throw new ArgumentNullException(nameof(turret));
            AdsViewer = adsViewer ?? throw new ArgumentNullException(nameof(adsViewer));
            RewardIssuer = rewardIssuer ?? throw new ArgumentNullException(nameof(rewardIssuer));
            PlayerController = playerController ?? throw new ArgumentNullException(nameof(playerController));
            VictoryController = victoryController ?? throw new ArgumentNullException(nameof(victoryController));
            ControllersAccess = controllersAccess ?? throw new ArgumentNullException(nameof(controllersAccess));
            LevelStatus = levelStatus ?? throw new ArgumentNullException(nameof(levelStatus));
        }

        public ITurret Turret { get; }

        public IAdsViewer AdsViewer { get; }

        public IRewardIssuer RewardIssuer { get; }

        public IPlayerController PlayerController { get; }

        public IVictoryController VictoryController { get; }

        public IActorsControllersAccess ControllersAccess { get; }

        public ILevelStatus LevelStatus { get; }
    }
}