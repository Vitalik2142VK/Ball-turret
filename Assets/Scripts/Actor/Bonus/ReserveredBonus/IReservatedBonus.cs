public interface IReservatedBonus : IBonus
{
    public bool IsCanActivate { get; }

    public void Initialize(IReservedBonusView view);

    public bool TryAddBonus(string nameBonus);
}
