using System;
using UnityEngine;

public interface ITurret : IDamagedObject, IEndPointStep
{
    public bool IsReadyShoot { get; }

    public void SetTouchPoint(Vector3 touchPoint);

    public void FixTargetPostion();

    public void RestoreHealth();
}
