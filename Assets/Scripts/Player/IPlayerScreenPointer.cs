using System;
using UnityEngine;

public interface IPlayerScreenPointer
{
    public event Action PressFinished;

    public Vector3 TouchPositionInMap { get; }
    public bool IsPress { get; }

    public void UpdateInput();
}