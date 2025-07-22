using System;
using UnityEngine;

public interface IBulletPhysics
{
    public event Action<GameObject> EnteredCollision;

    public void Activate();

    public void MoveToDirection(Vector3 direction);

    public void RecordPoint(float deltaTime);
}
