using System;
using UnityEngine;

public interface IGun
{
    public event Action Reloaded;
    public event Action ShotExecuted;

    public bool IsRecharged { get; }

    public void Shoot(Vector3 direction);
}