using UnityEngine;

public interface IMover
{
    public bool IsFinished { get; }

    public void SetStartPosition(Vector3 startPosition);

    public void SetParameters(Vector3 distance, float speed);

    public void Move();
}