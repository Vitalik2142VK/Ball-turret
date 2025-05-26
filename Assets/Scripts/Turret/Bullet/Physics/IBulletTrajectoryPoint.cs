using UnityEngine;

public interface IBulletTrajectoryPoint
{
    public Vector3 Velocity { get; }
    public Vector3 Position { get; }
    public GameObject CollidedGameObject { get; }
    public int Frame { get; }
    public bool IsThereCollision { get; }
}