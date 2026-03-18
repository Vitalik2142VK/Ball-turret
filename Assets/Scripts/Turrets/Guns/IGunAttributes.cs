namespace CannonTurret.Turrets.Guns
{
    public interface IGunAttributes
    {
        public float TimeBetweenShots { get; }
        public int InitialCountBulltes { get; }
    }
}