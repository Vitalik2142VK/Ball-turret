using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody), typeof(Mover))]
public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private Scriptable.DamageAttributes _damageAttributes;
    [SerializeField] private Scriptable.HealthAttributes _healthAttributes;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private DebuffReceiver _debuffReceiver;

    private IMover _mover;
    private IDamage _damage;
    private IHealth _health;

    public IMover Mover => _mover;
    public bool IsEnable { get; private set; }

    public string Name => name;

    private void OnValidate()
    {
        if (_damageAttributes == null)
            throw new NullReferenceException(nameof(_damageAttributes));

        if (_healthAttributes == null)
            throw new NullReferenceException(nameof(_healthAttributes));

        if (_healthBar == null)
            throw new NullReferenceException(nameof(_healthBar));

        if (_debuffReceiver == null)
            throw new NullReferenceException(nameof(_debuffReceiver));
    }

    private void Awake()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;

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

    public void AddDebuff(IDebuff debaff) => _debuffReceiver.AddDebuff(debaff);

    public void ActivateDebuffs()
    {
        _debuffReceiver.ActivateDebuffs();
        _debuffReceiver.RemoveCompletedDebuffs();
    }

    public void TakeDamage(IDamageAttributes damage)
    {
        _health.TakeDamage(damage);

        if (_health.IsAlive == false)
            Destroy();
    }

    public void ApplyDamage(IDamagedObject damagedObject) => _damage.Apply(damagedObject);

    public void Destroy()
    {
        _debuffReceiver.Clean();
        Destroy(gameObject);
    }

    public void SetStartPosition(Vector3 startPosition) => _mover.SetStartPosition(startPosition);
}
