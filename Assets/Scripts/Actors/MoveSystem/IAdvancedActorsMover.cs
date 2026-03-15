using System.Collections.Generic;

namespace CannonTurret.Actors.MoveSystem
{
    public interface IAdvancedActorsMover : IActorsMover
    {
        public void SetMoveAttributes(IMoveAttributes moveAttributes);

        public void SetMovableObjects(IEnumerable<IMovableObject> movableObjects);
    }
}