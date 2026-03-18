using UnityEngine;

namespace CannonTurret.Turrets.Bullets.Physics
{
    public interface IBulletTrajectoryPoint
    {
        public Collider CollidedObject { get; }

        public Vector3 Velocity { get; }

        public Vector3 Position { get; }

        public int Frame { get; }

        public bool IsThereCollision { get; }
    }
}