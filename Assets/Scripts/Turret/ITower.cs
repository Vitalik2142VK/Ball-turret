﻿using UnityEngine;

public interface ITower
{
    public Vector3 Direction { get; }
    public bool IsReadyShoot { get; }

    public void TakeAim(Vector3 targertPosition);

    public void SaveDirection();
}
