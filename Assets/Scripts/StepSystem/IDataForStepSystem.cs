using CannonTurret.Actors;
using CannonTurret.Coin.Rewards;
using CannonTurret.LevelSystem;
using CannonTurret.PlayerSystem;
using CannonTurret.SDK.Ads;
using CannonTurret.Turrets;

namespace CannonTurret.StepSystem
{
    public interface IDataForStepSystem
    {
        public ITurret Turret { get; }
        public IAdsViewer AdsViewer { get; }
        public IRewardIssuer RewardIssuer { get; }
        public IPlayerController PlayerController { get; }
        public IActorsControllersAccess ControllersAccess { get; }
        public ILevelStatus LevelStatus { get; }
        public IVictoryController VictoryController { get; }
    }
}