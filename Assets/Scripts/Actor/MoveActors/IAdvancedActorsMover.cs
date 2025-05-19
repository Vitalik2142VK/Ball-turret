using System.Collections.Generic;

public interface IAdvancedActorsMover : IActorsMover
{
    public void SetMoveAttributes(IMoveAttributes moveAttributes);

    public void SetMovableObjects(IEnumerable<IMovableObject> movableObjects);
}