using UnityEngine;

public interface IBulletPhysicsAttributes
{
    public float Speed { get; }
    public float Gravity { get; }
    public float BounceForce { get; }
    public LayerMask LayerMaskBounce { get; }
}

