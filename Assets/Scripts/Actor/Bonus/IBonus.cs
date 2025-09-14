public interface IBonus
{
    public IBonusCard BonusCard { get; }

    public void Initialize(IBonusActivator bonusActivator);

    public void Activate();

    public IBonusActivator GetCloneBonusActivator();
}