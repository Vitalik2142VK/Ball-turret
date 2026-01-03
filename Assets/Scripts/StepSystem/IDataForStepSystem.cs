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