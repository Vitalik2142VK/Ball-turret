public interface IViewableBonusCreator : IBonusCreator
{
    public string Name { get; }

    public IViewableBonus Create(IBonus bonus);
}