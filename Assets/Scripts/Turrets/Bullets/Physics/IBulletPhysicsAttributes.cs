using UnityEngine;

public interface IBulletPhysicsAttributes
{
    public float Speed { get; }
    public float Gravity { get; }
    public float BounceForce { get; }
    public float MinBounceAngle { get; }
    public float MaxBounceAngle { get; }
    public LayerMask LayerMaskBounce { get; }
}
