using CannonTurret.Actors.MoveSystem;

namespace CannonTurret.Actors
{
    public interface IActorsController : IActorsPreparator, IActorsMover, IActorsRemover, IDisableActorsRemover { }
}