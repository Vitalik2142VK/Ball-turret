using System;
using UnityEngine;

namespace CannonTurret.Turrets.Guns
{
    public interface IGun
    {
        public event Action Reloaded;
        public event Action ShotExecuted;

        public bool IsRecharged { get; }

        public void Shoot(Vector3 direction);
    }
}