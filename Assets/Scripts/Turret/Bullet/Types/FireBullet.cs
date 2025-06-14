﻿using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet), typeof(FireBulletDebuff), typeof(BulletPhysics))]
public class FireBullet : MonoBehaviour, IBullet, IInitializer
{
    [SerializeField] private Scriptable.DamageImproverAttributes _damageImproverAttributes;

    private IBullet _bullet;
    private IBulletDebuff _bulletDebaff;
    private IBulletPhysics _bulletPhysics;

    public BulletType BulletType => _bullet.BulletType;

    private void OnValidate()
    {
        if (_damageImproverAttributes == null)
            throw new System.NullReferenceException(nameof(_damageImproverAttributes));
    }

    private void Awake()
    {
        _bulletPhysics = GetComponent<IBulletPhysics>();
    }

    private void OnEnable()
    {
        _bulletPhysics.EnteredCollision += OnApplyDebaff;
    }

    private void OnDisable()
    {
        _bulletPhysics.EnteredCollision -= OnApplyDebaff;
    }

    public void Initialize()
    {
        Bullet bullet = GetComponent<Bullet>();
        FireBulletDebuff bulletDebaff = GetComponent<FireBulletDebuff>();

        bullet.ChangeDamage(_damageImproverAttributes);
        bulletDebaff.Initialize(bullet.DamageAttributes);

        _bullet = bullet;
        _bulletDebaff = bulletDebaff;
    }

    public void Move(Vector3 startPoint, Vector3 direction) => _bullet.Move(startPoint, direction);

    public void SetActive(bool isActive) => _bullet.SetActive(isActive);

    public void Gather(IBonus bonus) => _bullet.Gather(bonus);

    public bool TryGetBonuses(out IReadOnlyCollection<IBonus> bonuses) => _bullet.TryGetBonuses(out bonuses);

    private void OnApplyDebaff(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out IEnemy enemy))
            _bulletDebaff.ApplyDebuff(enemy);
    }
}
