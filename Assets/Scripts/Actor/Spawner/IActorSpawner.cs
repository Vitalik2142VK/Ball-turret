using System.Collections.Generic;

public interface IActorSpawner
{
    public List<IActor> Spawn(IWaveActorsPlanner planner);
}