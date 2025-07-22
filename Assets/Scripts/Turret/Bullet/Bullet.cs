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
    private ISound _sound;

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

    public void Initialize(IDamageAttributes damageBulletAttributes, ISound sound)
    {
        DamageAttributes = damageBulletAttributes ?? throw new ArgumentNullException(nameof(damageBulletAttributes));

        _sound = sound ?? throw new ArgumentNullException(nameof(sound));

        _damage = new Damage(DamageAttributes);
        _gatherer = new BonusGathererBullet();
    }
 
    public void ChangeDamage(IDamageImproverAttributes damageImproverAttributes)
    {
        if (damageImproverAttributes == null)
            throw new ArgumentNullException(nameof(damageImproverAttributes));

        var damageChanger = new DamageChanger(DamageAttributes);
        damageChanger.Change(damageImproverAttributes);
        DamageAttributes = damageChanger;

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
        _sound.Play();

        if (gameObject.TryGetComponent(out IDamagedObject damagedObject))
            _damage.Apply(damagedObject);
    }
}
