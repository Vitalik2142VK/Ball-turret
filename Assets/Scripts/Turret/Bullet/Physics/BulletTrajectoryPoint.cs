using System;
using UnityEngine;

public class BulletTrajectoryPoint : IBulletTrajectoryPoint
{
    public BulletTrajectoryPoint(int frame)
    {
        if (frame < 0)
            throw new ArgumentOutOfRangeException(nameof(frame));

        Frame = frame;

        IsThereCollision = false;
    }

    public BulletTrajectoryPoint(int frame, Vector3 velocity, Vector3 position) : this(frame)
    {
        Velocity = velocity;
        Position = position;
    }

    public Vector3 Velocity { get; set; }
    public Vector3 Position { get; set; }
    public Collider CollidedObject { get; private set; }
    public int Frame { get; private set; }
    public bool IsThereCollision { get; private set; }

    public void SetCollidedObject(Collider collidedObject)
    {
        CollidedObject = collidedObject != null ? collidedObject : throw new ArgumentNullException(nameof(collidedObject));
        IsThereCollision = true;
    }
}
