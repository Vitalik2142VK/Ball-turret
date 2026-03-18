using System;

namespace CannonTurret.Actors.Bonuses.ReserveredBonuses
{
    public class ReservatedBonus : IReservatedBonus
    {
        private readonly IBonus Bonus;
        private readonly ReservatedBonusData Data;

        private IReservedBonusView _view;

        public ReservatedBonus(IBonus bonus, int maxBonusesCount)
        {
            if (maxBonusesCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxBonusesCount));

            Bonus = bonus ?? throw new ArgumentNullException(nameof(bonus));
            Data = new ReservatedBonusData(maxBonusesCount);
        }

        public IBonusCard BonusCard => Bonus.BonusCard;

        public bool IsCanActivate => Data.IsCanActivate;

        public void Initialize(IReservedBonusView view)
        {
            if (_view == null)
            {
                _view = view ?? throw new ArgumentNullException(nameof(view));
                _view.Initialize(Data);
                _view.UpdateData();
            }
        }

        public void Activate()
        {
            if (IsCanActivate == false)
                throw new InvalidOperationException();

            Data.CurrentBonusesCount--;
            Bonus.Activate();
            _view.UpdateData();
        }

        public bool TryAddBonus(string nameBonus)
        {
            if (string.IsNullOrEmpty(nameBonus))
                throw new ArgumentException(nameof(nameBonus));

            if (nameBonus == BonusCard.Name && Data.IsFull == false)
            {
                Data.CurrentBonusesCount++;
                _view.UpdateData();

                return true;
            }

            return false;
        }
    }
}