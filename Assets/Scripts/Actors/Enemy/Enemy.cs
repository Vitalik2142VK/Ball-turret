using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private DamageAttributes _damageAttributes;
    [SerializeField] private HealthAttributes _healthAttributes;
    [SerializeField] private HealthBar _healthBar;

    private IMover _mover;
    private IDamage _damage;
    private IHealth _health;

    public IMover Mover => _mover;
    public bool IsEnable { get; private set; }

    private void OnValidate()
    {
        if (_damageAttributes == null)
            throw new NullReferenceException(nameof(_damageAttributes));

        if (_healthAttributes == null)
            throw new NullReferenceException(nameof(_healthAttributes));

        if (_healthBar == null)
            throw new NullReferenceException(nameof(_healthBar));
    }

    private void Awake()
    {
        _mover = GetComponent<Mover>();

        _damage = new Damage(_damageAttributes);
        _health = new Health(_healthAttributes, _healthBar);
    }

    private void OnEnable()
    {
        IsEnable = true;
    }

    private void Start()
    {
        _health.Restore();
    }

    private void OnDisable()
    {
        IsEnable = false;
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);

        if (_health.IsAlive == false)
            Destroy();
    }

    public void ApplyDamage(IDamagedObject damagedObject)
    {
        _damage.Apply(damagedObject);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
