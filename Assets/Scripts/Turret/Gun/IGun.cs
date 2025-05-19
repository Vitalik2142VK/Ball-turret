using System;
using UnityEngine;

public interface IGun
{
    public event Action ShootingCompleted;

    public bool IsRecharged { get; }

    public void TakeAim(Vector3 direction);

    public void Shoot(Vector3 direction);
}