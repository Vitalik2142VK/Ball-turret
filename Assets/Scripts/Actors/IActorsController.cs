public interface IActorsController
{
    public IActorsMover ActorsMover { get; }
    public IActorsRemover ActorsRemover { get; }
    public IEnemiesAttacker EnemyAttacker { get; }

    public void PrepareActors();
}
