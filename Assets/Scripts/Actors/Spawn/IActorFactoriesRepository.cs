namespace CannonTurret.Actors.Spawn
{
    public interface IActorFactoriesRepository
    {
        public IActorFactory GetFactoryByNameTypeActor(string nameTypeActor);
    }
}