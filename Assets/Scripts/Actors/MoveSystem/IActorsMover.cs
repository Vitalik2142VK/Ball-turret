namespace CannonTurret.Actors.MoveSystem
{
    public interface IActorsMover
    {
        public bool AreMovesFinished { get; }

        public void MoveAll();
    }
}