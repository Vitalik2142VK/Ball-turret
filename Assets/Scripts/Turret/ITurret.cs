using UnityEngine;

public interface ITurret
{
    public bool IsInProcessShooting { get; }

    public void SetTouchPoint(Vector3 touchPoint);

    public void FixTargetPostion();
}
