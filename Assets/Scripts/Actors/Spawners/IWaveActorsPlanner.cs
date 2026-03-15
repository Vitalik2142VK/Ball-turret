using System.Collections.Generic;

public interface IWaveActorsPlanner
{
    public IReadOnlyCollection<IActorPlanner> GetActorPlanners();
}
