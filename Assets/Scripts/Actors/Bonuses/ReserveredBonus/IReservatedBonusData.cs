public interface IReservatedBonusData
{
    public int MaxBonusesCount { get; }
    public int CurrentBonusesCount { get; }
    public bool IsCanActivate { get; }
}
