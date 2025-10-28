using System.Collections.Generic;

public interface IAdvancedActorPreparator : IActorsPreparator
{
    public IActorsMover ActorsMover { get; }
    public int EnemiesCount { get; }
    public bool AreWavesOver { get; }

    public void SetLevelActorsPlanner(ILevel level);

    public List<IActor> PopActors();

    public void CountRemainingEnemies();

    public void ActivateDebuffablies();
}
