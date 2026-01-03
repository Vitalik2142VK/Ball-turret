public class ReservatedBonusData : IReservatedBonusData
{
    private int _currentBonusesCount;

    public ReservatedBonusData(int maxBonusesCount)
    {
        MaxBonusesCount = maxBonusesCount;
        _currentBonusesCount = 0;
    }

    public int MaxBonusesCount { get; }

    public bool IsCanActivate => CurrentBonusesCount > 0;
    public bool IsFull => CurrentBonusesCount >= MaxBonusesCount;

    public int CurrentBonusesCount
    {
        get
        {
            return _currentBonusesCount;
        }

        set
        {
            if (value < 0 || value > MaxBonusesCount)
                throw new System.ArgumentOutOfRangeException(nameof(CurrentBonusesCount));
            else
                _currentBonusesCount = value;
        }
    }
}