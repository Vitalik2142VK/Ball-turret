public interface IReservedBonusView
{
    public bool IsCanActivate {  get; }

    public void Initialize(IReservatedBonusData reservatedBonusData);

    public void UpdateData();
}
