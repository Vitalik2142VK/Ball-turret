using System;
using UnityEngine;

namespace CannonTurret.Effects.Freezing
{
    public interface IIceShell
    {
        public event Action Disabled;

        public void SetScale(Vector3 scale);

        public void SetPosition(Vector3 position);

        public void Enable();

        public void Disable();
    }
}