namespace CannonTurret.Actors.Spawn
{
    public interface IActorFactory
    {
        public bool CanCreate(string nameTypeActor);

        public IActor Create(string nameTypeActor);
    }
}