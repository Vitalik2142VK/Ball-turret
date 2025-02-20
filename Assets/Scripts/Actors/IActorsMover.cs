using System.Collections.Generic;

public interface IActorsMover
{
    public bool AreMovesFinished { get; }

    public void SetMovableObject(IEnumerable<IMovableObject> movableObjects);

    public void MoveAll();
}
