public interface IBonusCreator
{
    public void Initialize(IBonusActivator bonusActivator);

    public IBonus Create();
}
