public interface IViewableBonus : IBonus, IActor, IActorModel 
{
    public void HandleBonusGatherer(IBonusGatherer bonusGatherer);
}
