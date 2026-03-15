using UnityEngine;

public class SpawnPoint
{
    private Vector3 _position;

    public SpawnPoint(Vector3 position)
    {
        _position = position;
        IsFree = true;
    }

    public bool IsFree { get; private set; }

    public Vector3 GetPosition()
    {
        IsFree = false;

        return _position;
    }

    public void FreePoint()
    {
        IsFree = true;
    }
}