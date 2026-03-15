using CannonTurret.Actors.MoveSystem;
using CannonTurret.HealthSystem;

namespace CannonTurret.Actors
{
    public interface IActor : IMovableObject, IDestroyedObject
    {
        public bool IsEnable { get; }
    }
}