using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody), typeof(Mover))]
public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private Scriptable.EnemyAttributes _enemyAttributes;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private DebuffReceiver _debuffReceiver;

    private IMover _mover;
    private IDamage _damage;
    private IHealth _health;
    private ISound _sound;

    public IMover Mover => _mover;
    public bool IsEnable { get; private set; }

    public string Name => name;

    private void OnValidate()
    {
        if (_enemyAttributes == null)
            throw new NullReferenceException(nameof(_enemyAttributes));

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

    public void Initialize(ISound sound)
    {
        _sound = sound ?? throw new ArgumentNullException(nameof(sound));

        _damage = new Damage(_enemyAttributes);
        _health = new Health(_enemyAttributes, _healthBar);
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
        {
            _sound.Play();

            Destroy();
        }
    }

    public void ApplyDamage(IDamagedObject damagedObject) => _damage.Apply(damagedObject);

    public void Destroy()
    {
        _debuffReceiver.Clean();
        Destroy(gameObject);
    }

    public void SetStartPosition(Vector3 startPosition) => _mover.SetStartPosition(startPosition);
}
