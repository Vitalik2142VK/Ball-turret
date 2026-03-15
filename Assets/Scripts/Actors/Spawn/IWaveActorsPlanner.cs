using System.Collections.Generic;

namespace CannonTurret.Actors.Spawn
{
    public interface IWaveActorsPlanner
    {
        public IReadOnlyCollection<IActorPlanner> GetActorPlanners();
    }
}