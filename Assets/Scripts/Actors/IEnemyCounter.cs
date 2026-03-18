namespace CannonTurret.Actors
{
    public interface IEnemyCounter
    {
        public bool AreNoEnemies { get; }

        public void Count();
    }
}