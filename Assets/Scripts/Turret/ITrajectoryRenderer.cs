using UnityEngine;

public interface ITrajectoryRenderer
{
    public void ShowTrajectory(Vector3 origin, Vector3 direction);

    public void Enable();

    public void Disable();
}