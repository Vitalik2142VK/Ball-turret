using UnityEngine;

public interface IMovableObject
{
    public IMover Mover { get; }

    public void SetStartPosition(Vector3 startPosition);
}
