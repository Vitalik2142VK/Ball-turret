using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody), typeof(Mover))]
public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private Scriptable.EnemyAttributes _enemyAttributes;
    [SerializeField, SerializeIterface(typeof(IDebuffReceiver))] private GameObject _debuffReceiverGameObject;
    [SerializeField] private HealthBar _healthBar;

    private IDebuffReceiver _debuffReceiver;
    private IMover _mover;
    private IDamage _damage;
    private IHealth _health;
    private ISound _sound;

    public IMover Mover => _mover;
    public string Name => name;

    public bool IsEnable { get; private set; }

    private void OnValidate()
    {
        if (_enemyAttributes == null)
            throw new NullReferenceException(nameof(_enemyAttributes));

        if (_healthBar == null)
            throw new NullReferenceException(nameof(_healthBar));

        if (_debuffReceiverGameObject == null)
            throw new NullReferenceException(nameof(_debuffReceiverGameObject));
    }

    private void Awake()
    {
        _debuffReceiver = _debuffReceiverGameObject.GetComponent<IDebuffReceiver>();

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;

        _mover = GetComponent<Mover>();
    }

    private void OnEnable()
    {
        IsEnable = true;

        _health?.Restore();
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
        _health.Restore();
    }

    public void AddDebuff(IDebuff debaff) => _debuffReceiver.AddDebuff(debaff);

    public void ApplyDamage(IDamagedObject damagedObject) => _damage.Apply(damagedObject);

    public void SetStartPosition(Vector3 startPosition) => _mover.SetStartPosition(startPosition);

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

    public void Destroy()
    {
        _sound.Play();
        _debuffReceiver.Clean();
        Destroy(gameObject);
    }
}
