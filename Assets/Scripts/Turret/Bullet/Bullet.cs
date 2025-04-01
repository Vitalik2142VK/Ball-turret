using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletPhysics))]
public abstract class Bullet : MonoBehaviour, IBullet
{
    [SerializeField] private DamageAttributes _damageAttributes;
    [SerializeField] private BulletPhysicsAttributes _physicsAttributes;

    private Transform _transform;
    private IDamage _damage;
    private IBulletPhysics _bulletPhysics;
    private IBonusGatherer _gatherer;

    public event Action<IBullet> Finished;

    public abstract BulletType BulletType { get; }

    private void OnValidate()
    {
        if (_damageAttributes == null)
            throw new NullReferenceException(nameof(_damageAttributes));

        if (_physicsAttributes == null)
            throw new NullReferenceException(nameof(_physicsAttributes));
    }

    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        _bulletPhysics.UseGravity();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _bulletPhysics.HandleCollision(collision);

        ApplyDamage(collision);
    }

    public void Initialize(IDamageImprover damageImprover)
    {
        if (damageImprover == null)
            throw new ArgumentNullException(nameof(damageImprover));

        BulletPhysics bulletPhysics = GetComponent<BulletPhysics>();
        bulletPhysics.Initialize(_physicsAttributes);
        _bulletPhysics = bulletPhysics;

        damageImprover.Improve(_damageAttributes);
        _damage = new Damage(damageImprover);
        _gatherer = new BonusGathererBullet();
    }
 
    public void Move(Vector3 startPoint, Vector3 direction)
    {
        _transform.position = startPoint;
        _bulletPhysics.MoveToDirection(direction);
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void EndFlight()
    {
        Finished?.Invoke(this);
    }

    public void Gather(IBonus bonus)
    {
        _gatherer.Gather(bonus);
    }

    public bool TryGetBonuses(out List<IBonus> bonuses)
    {
        return _gatherer.TryGetBonuses(out bonuses);
    }

    private void ApplyDamage(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamagedObject damagedObject))
            _damage.Apply(damagedObject);
    }
}
