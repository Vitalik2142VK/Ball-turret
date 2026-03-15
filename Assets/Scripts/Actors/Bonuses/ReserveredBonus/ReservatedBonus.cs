using System;

public class ReservatedBonus : IReservatedBonus
{
    private IBonus _bonus;
    private IReservedBonusView _view;
    private ReservatedBonusData _data;

    public ReservatedBonus(IBonus bonus, int maxBonusesCount)
    {
        if (maxBonusesCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxBonusesCount));

        _bonus = bonus ?? throw new ArgumentNullException(nameof(bonus));
        _data = new ReservatedBonusData(maxBonusesCount);
    }

    public IBonusCard BonusCard => _bonus.BonusCard;
    public bool IsCanActivate => _data.IsCanActivate;

    public void Initialize(IReservedBonusView view)
    {
        if (_view == null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _view.Initialize(_data);
            _view.UpdateData();
        }
    }

    public void Activate()
    {
        if (IsCanActivate == false)
            throw new InvalidOperationException();

        _data.CurrentBonusesCount--;
        _bonus.Activate();
        _view.UpdateData();
    }

    public bool TryAddBonus(string nameBonus)
    {
        if (string.IsNullOrEmpty(nameBonus))
            throw new ArgumentException(nameof(nameBonus));

        if (nameBonus == BonusCard.Name && _data.IsFull == false)
        {
            _data.CurrentBonusesCount++;
            _view.UpdateData();

            return true;
        }

        return false;
    }
}
