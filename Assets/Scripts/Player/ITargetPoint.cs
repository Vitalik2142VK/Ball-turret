using UnityEngine;

public interface ITargetPoint
{
    public bool IsInsideZoneEnemy { get; }
    public Vector3 Position { get; }

    public void SetPosition(Vector3 position);

    public void SaveLastPosition();
}
