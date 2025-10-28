using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BonusTrigger : MonoBehaviour
{
    public event Action<Collider> Activated;

    private void Awake()
    {
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Activated?.Invoke(other);
    }

    public void SetAvtive(bool isActive) => gameObject.SetActive(isActive);
}