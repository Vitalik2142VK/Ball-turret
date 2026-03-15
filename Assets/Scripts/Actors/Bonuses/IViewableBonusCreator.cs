namespace CannonTurret.Actors.Bonuses
{
    public interface IViewableBonusCreator : IBonusCreator
    {
        public IViewableBonus Create(IBonus bonus);
    }
}