using System.Collections.Generic;

public interface IAdvancedActorPreparator : IActorsPreparator
{
    public IActorsMover ActorsMover { get; }
    public int EnemiesCount { get; }
    public bool AreWavesOver { get; }

    public IEnumerable<IActor> PopActors();

    public void CountRemainingEnemies();

    public void ActivateDebuffablies();

    public void SetLevel(ILevel level);

    public IEnumerable<IEnemy> GetEnemies();
}
