using System.Collections.Generic;

namespace CannonTurret.Actors.Spawn
{
    public interface IActorSpawner
    {
        public List<IActor> Spawn(IWaveActorsPlanner planner);
    }
}