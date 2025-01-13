using UnityEngine;

public interface IBulletPhysics
{
    public void UseGravity();

    public void MoveToDirection(Vector3 direction);

    public void HandleCollision(Collision collision);
}
