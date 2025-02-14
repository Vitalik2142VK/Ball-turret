using UnityEngine;

public interface IGun
{
    public bool IsShooting { get; }

    public void Shoot(Vector3 direction);
}