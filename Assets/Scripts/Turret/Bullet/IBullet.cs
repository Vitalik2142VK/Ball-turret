using System;
using UnityEngine;

public interface IBullet
{
    public event Action<IBullet> Finished;

    public BulletType BulletType { get; }

    public void Move(Vector3 startPoint, Vector3 direction);

    public void SetActive(bool isActive);

    public void EndFlight();
}
