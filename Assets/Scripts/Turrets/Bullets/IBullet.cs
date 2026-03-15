using CannonTurret.Actors.Bonuses;
using CannonTurret.Turrets.Bullets.Types;
using UnityEngine;

namespace CannonTurret.Turrets.Bullets
{
    public interface IBullet : IBonusGatherer
    {
        public BulletType BulletType { get; }

        public void Move(Vector3 startPoint, Vector3 direction);

        public void SetActive(bool isActive);
    }
}