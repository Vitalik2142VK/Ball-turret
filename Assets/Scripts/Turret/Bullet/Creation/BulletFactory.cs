using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletFactory : MonoBehaviour, IBulletFactory
{
    [SerializeField] private Transform _containerBullets;
    [SerializeField] private Bullet[] _bulletPrefabs;

    private Dictionary<BulletType, Bullet> _bullets;
    private IDamageAttributes _damageBulletAttributes;

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

    public void Initialize(IDamageAttributes damageBulletAttributes)
    {
        _damageBulletAttributes = damageBulletAttributes ?? throw new ArgumentNullException(nameof(damageBulletAttributes));
    }

    public void AddPrefab(Bullet bullet)
    {
        if (bullet == null)
            throw new ArgumentNullException(nameof(bullet));

        _bullets.Add(bullet.BulletType, bullet);
    }

    public IBullet Create(BulletType type)
    {
        if (_bullets.ContainsKey(type) == false)
            throw new ArgumentOutOfRangeException(nameof(type));

        Bullet bullet = Instantiate(_bullets[type]);
        bullet.Initialize(_damageBulletAttributes);
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(_containerBullets);

        if (type == BulletType.Bomb)
            InitializeBomb(bullet);

        return bullet;
    }

    private void InitializeBomb(Bullet bullet)
    {
        if (bullet.TryGetComponent(out ExplodingBullet explodingBullet) && _bullets[BulletType.Bomb].TryGetComponent(out ExplodingBullet bulletPrefab))
            explodingBullet.Initialize(bulletPrefab.BulletRepository);
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
