using UnityEngine;

namespace CannonTurret.PlayerSystem
{
    [RequireComponent(typeof(Collider))]
    public class ZoneShot : MonoBehaviour
    {
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public bool IsPointInside(Vector3 point)
        {
            return _collider.bounds.Contains(point);
        }
    }
}