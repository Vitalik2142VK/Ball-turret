public interface IChoiceBonusButton
{
    public IBonus Bonus { get; }

    public void Enable();

    public void Disable();
}