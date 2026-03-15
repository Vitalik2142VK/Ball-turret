using System;
using UnityEngine;

[RequireComponent(typeof(BulletPhysics))]
public class BulletCountingCombo : MonoBehaviour
{
    private IBulletPhysics _bulletPhysics;
    private IComboCounter _comboCounter;

    private void Awake()
    {
        _bulletPhysics = GetComponent<IBulletPhysics>();
    }

    private void OnEnable()
    {
        _bulletPhysics.EnteredCollision += OnCount;
    }

    private void OnDisable()
    {
        _bulletPhysics.EnteredCollision -= OnCount;
    }

    public void Initialize(IComboCounter comboCounter)
    {
        _comboCounter = comboCounter ?? throw new ArgumentNullException(nameof(comboCounter));
    }

    private void OnCount(Collider collider)
    {
        if (collider.TryGetComponent(out IEnemyView _))
            _comboCounter.Count();
    }
}
