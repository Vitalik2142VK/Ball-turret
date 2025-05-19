public interface IBonus
{
    public IBonusCard BonusCard { get; }

    public void Activate();

    public IBonusActivator GetCloneBonusActivator();
}