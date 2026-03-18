using System;
using UnityEngine;

namespace CannonTurret.CameraControl
{
    public interface ICameraAdapter
    {
        public event Action OrientationChanged;
        public event Action RatioChanged;

        public Vector3 Rotation { get; }
        public bool IsPortraitOrientation { get; }
    }
}