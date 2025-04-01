using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour, IBulletFactory
{
    [SerializeField] private Transform _containerBullets;
    [SerializeField] private Bullet[] _bulletPrefabs;

    private Dictionary<BulletType, Bullet> _bullets;
    private IDamageImprover _damageImprover;

    private void OnValidate()
    {
        if (_containerBullets == null)
            throw new NullReferenceException(nameof(_containerBullets));
    }

    private void Awake()
    {
        if (_bulletPrefabs == null)
            throw new NullReferenceException(nameof(_bulletPrefabs));

        if (_bulletPrefabs.Length == 0)
            throw new InvalidOperationException(nameof(_bulletPrefabs));

        _bullets = CreateDictionaryPrefabs();
    }

    public void Initialize(IDamageImprover damageImprover)
    {
        _damageImprover = damageImprover ?? throw new ArgumentNullException(nameof(damageImprover));
    }

    public IBullet Create(BulletType type)
    {
        if (_bullets.ContainsKey(type) == false)
            throw new ArgumentOutOfRangeException(nameof(type));

        Bullet bullet = Instantiate(_bullets[type]);
        bullet.Initialize(_damageImprover);
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(_containerBullets);

        return bullet;
    }

    private Dictionary<BulletType, Bullet> CreateDictionaryPrefabs()
    {
        Dictionary<BulletType, Bullet> prefabs = new Dictionary<BulletType, Bullet>(_bulletPrefabs.Length);

        foreach (Bullet bullet in _bulletPrefabs)
        {
            if (bullet == null)
                throw new NullReferenceException(nameof(bullet));

            prefabs.Add(bullet.BulletType, bullet);
        }

        return prefabs;
    }
}
