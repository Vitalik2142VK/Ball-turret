using System.Collections.Generic;

public interface ILineActorPlaner
{
    public IEnumerable<IActorPlanner> GetActorPlanners(int lineNumber);
}
