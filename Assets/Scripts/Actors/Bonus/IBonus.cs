public interface IBonus : IActor
{
    public void SetBonusActivator(IBonusActivator bonusActivator);

    public void Activate();
}