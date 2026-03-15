using UnityEngine;

namespace CannonTurret.Turrets
{
    public interface ITrajectoryRenderer
    {
        public void Enable();

        public void Disable();

        public void ShowTrajectory(Vector3 origin, Vector3 direction);

        public void Clear();
    }
}