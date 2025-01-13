using System;
using UnityEngine;

public class ZoneEnemy : MonoBehaviour
{
    public event Action TargetPointExited;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out TargetPoint _))
            TargetPointExited?.Invoke();
    }
}
