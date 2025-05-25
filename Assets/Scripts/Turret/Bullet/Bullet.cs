using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletPhysics))]
public class Bullet : MonoBehaviour, IBullet
{
    [SerializeField] private BulletType _bulletType;

    private Transform _transform;
    private IDamage _damage;
    private IBulletPhysics _bulletPhysics;
    private IBonusGatherer _gatherer;

    public BulletType BulletType => _bulletType;

    public IDamageAttributes DamageAttributes { get; private set; }

    private void Awake()
    {
        _transform = transform;

        _bulletPhysics = GetComponent<IBulletPhysics>();
    }

    private void OnEnable()
    {
        _bulletPhysics.EnteredCollision += OnApplyDamage;
    }

    private void FixedUpdate()
    {
        _bulletPhysics.Activate();
    }

    private void OnDisable()
    {
        _bulletPhysics.EnteredCollision -= OnApplyDamage;
    }

    public void Initialize(IDamageAttributes damageBulletAttributes)
    {
        DamageAttributes = damageBulletAttributes ?? throw new ArgumentNullException(nameof(damageBulletAttributes));

        _damage = new Damage(DamageAttributes);
        _gatherer = new BonusGathererBullet();
    }
 
    public void ChangeDamage(IDamageImproverAttributes damageImproverAttributes)
    {
        if (damageImproverAttributes == null)
            throw new ArgumentNullException(nameof(damageImproverAttributes));

        IDamageImprover damageImprover = new DamageImprover(DamageAttributes);
        damageImprover.Improve(damageImproverAttributes);
        DamageAttributes = damageImprover;

        _damage = new Damage(DamageAttributes);
    }

    public void Move(Vector3 startPoint, Vector3 direction)
    {
        _transform.position = startPoint;
        _bulletPhysics.MoveToDirection(direction);
    }

    public void SetActive(bool isActive) => gameObject.SetActive(isActive);

    public void Gather(IBonus bonus) => _gatherer.Gather(bonus);

    public bool TryGetBonuses(out IReadOnlyCollection<IBonus> bonuses)
    {
        return _gatherer.TryGetBonuses(out bonuses);
    }

    private void OnApplyDamage(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out IDamagedObject damagedObject))
            _damage.Apply(damagedObject);
    }
}
