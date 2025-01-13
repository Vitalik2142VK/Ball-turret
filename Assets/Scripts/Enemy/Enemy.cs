using System;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
public class Enemy : MonoBehaviour, IDamagedObject
{
    [SerializeField] private HealthAttributes _attributes;
    [SerializeField] private EnemyHealthBar _healthBar;

    private IHealth _health;

    private void OnValidate()
    {
        if (_attributes == null)
            throw new NullReferenceException(nameof(_attributes));

        if (_healthBar == null)
            throw new NullReferenceException(nameof(_healthBar));
    }

    private void Awake()
    {
        _health = new Health(_attributes, _healthBar);
    }

    private void OnEnable()
    {
        _health.Died += OnDie;
    }

    private void Start()
    {
        _health.Restore();
    }

    private void OnDisable()
    {
        _health.Died -= OnDie;
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    private void OnDie()
    {
        Destroy(gameObject);
    }
}
