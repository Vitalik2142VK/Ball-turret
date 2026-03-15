namespace CannonTurret.Actors
{
    public interface IDebuffable : IDebuffReceiver
    {
        public void ActivateDebuffs();
    }
}