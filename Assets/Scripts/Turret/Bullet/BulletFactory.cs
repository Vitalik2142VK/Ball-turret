using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] private Bullet[] _bulletPrefabs;

    private Dictionary<BulletType, Bullet> _bullets;

    private void Awake()
    {
        if (_bulletPrefabs == null)
            throw new NullReferenceException(nameof(_bulletPrefabs));

        if (_bulletPrefabs.Length == 0)
            throw new InvalidOperationException(nameof(_bulletPrefabs));

        _bullets = new Dictionary<BulletType, Bullet>();

        foreach (Bullet bullet in _bulletPrefabs)
        {
            if (bullet == null)
                throw new NullReferenceException(nameof(bullet));

            _bullets.Add(bullet.BulletType, bullet);
        }
    }

    public IBullet Create(BulletType type)
    {
        if (_bullets.ContainsKey(type) == false)
            throw new ArgumentOutOfRangeException(nameof(type));

        Bullet bullet = Instantiate(_bullets[type]);
        bullet.gameObject.SetActive(false);

        return bullet;
    }
}
