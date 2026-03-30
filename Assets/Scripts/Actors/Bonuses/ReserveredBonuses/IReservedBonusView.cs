namespace CannonTurret.Actors.Bonuses.ReserveredBonuses
{
    public interface IReservedBonusView
    {
        public void Initialize(IReservatedBonusData reservatedBonusData);

        public void UpdateData();
    }
}