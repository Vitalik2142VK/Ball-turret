using UnityEngine;

public interface ITurret : IDamagedObject
{
    public bool IsInProcessShooting { get; }

    public void SetTouchPoint(Vector3 touchPoint);

    public void FixTargetPostion();
}
