using UnityEngine;

namespace CannonTurret.Effects
{
    public interface IExploder
    {
        public void Explode(Vector3 pointContact);
    }
}