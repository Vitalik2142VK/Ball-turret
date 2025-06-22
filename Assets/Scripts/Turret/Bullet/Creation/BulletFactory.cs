using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour, IBulletFactory
{
    [SerializeField] private Transform _containerBullets;
    [SerializeField] private Bullet[] _bulletPrefabs;

    private Dictionary<BulletType, Bullet> _bullets;
    private IDamageAttributes _damageBulletAttributes;
    private ISound _hitBulletSound;

    private void OnValidate()
    {
        if (_containerBullets == null)
            _containerBullets = transform;
    }

    private void Awake()
    {
        if (_bulletPrefabs == null)
            throw new NullReferenceException(nameof(_bulletPrefabs));

        if (_bulletPrefabs.Length == 0)
            throw new InvalidOperationException(nameof(_bulletPrefabs));

        _bullets = CreateDictionaryPrefabs();
    }

    public void Initialize(IDamageAttributes damageBulletAttributes, ISound hitBulletSound)
    {
        _damageBulletAttributes = damageBulletAttributes ?? throw new ArgumentNullException(nameof(damageBulletAttributes));
        _hitBulletSound = hitBulletSound ?? throw new ArgumentNullException(nameof(hitBulletSound));
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

        Bullet prefab = _bullets[type];
        Bullet bullet = Instantiate(prefab);
        bullet.Initialize(_damageBulletAttributes, _hitBulletSound);
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(_containerBullets);

        InitializeSpecialBullet(prefab, bullet);

        return bullet;
    }

    private void InitializeSpecialBullet(Bullet prefab, Bullet bullet)
    {
        var initializes = prefab.GetComponents<IBulletInitializer>();

        if (initializes == null || initializes.Length == 0)
            return;

        foreach (var initializer in initializes)
            initializer.Initialize(bullet);
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
