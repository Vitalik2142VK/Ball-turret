using System;
using UnityEngine;

public interface ICameraAdapter
{
    public event Action OrientationChanged;

    public Vector3 Rotation { get; }
    public bool IsPortraitOrientation { get; }
}