using UnityEngine;

namespace CannonTurret.Actors.MoveSystem
{
    public interface IMoveAttributes
    {
        public Vector3 Distance { get; }
        public float Speed { get; }
    }
}