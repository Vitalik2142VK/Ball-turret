using UnityEngine;

public interface IMovableObject
{
    public bool IsFinished { get; }

    public void SetStartPosition(Vector3 startPosition);

    public void SetPoint(Vector3 distance, float speed);

    public void Move();
}