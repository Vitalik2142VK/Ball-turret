using System;
using UnityEngine;

public interface ITurret : IDamagedObject, IEndPointStep, ITurretState
{
    public bool IsReadyShoot { get; }

    public void SetTouchPoint(Vector3 touchPoint);

    public void FixTargetPostion(Vector3 targetPostion);

    public void RestoreHealth();
}
