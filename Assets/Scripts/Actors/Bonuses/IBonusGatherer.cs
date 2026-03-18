namespace CannonTurret.Actors.Bonuses
{
    public interface IBonusGatherer : IBonusStorage
    {
        public void Gather(IBonus bonus);
    }
}