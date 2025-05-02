public interface IBonus : IActor
{
    public IBonusCard BonusCard { get; }

    public void Activate();

    public IBonusActivator GetCloneBonusActivator();
}