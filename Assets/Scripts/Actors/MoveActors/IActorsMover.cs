using System.Collections.Generic;

public interface IActorsMover
{
    public bool AreMovesFinished { get; }

    public void SetMoveAttributes(IMoveAttributes moveAttributes);

    public void SetMovableObjects(IEnumerable<IMovableObject> movableObjects);

    public void MoveAll();
}
