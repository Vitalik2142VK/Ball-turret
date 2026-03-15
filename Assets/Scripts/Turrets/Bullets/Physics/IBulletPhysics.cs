using System;
using UnityEngine;

namespace CannonTurret.Turrets.Bullets.Physics
{
    public interface IBulletPhysics
    {
        public event Action<Collider> EnteredCollision;

        public void Activate();

        public void MoveToDirection(Vector3 direction);

        public void RecordPoint(float deltaTime);
    }
}