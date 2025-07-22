using System;
using UnityEngine;

public class ExplodingBulletInitializer : MonoBehaviour, IBulletInitializer
{
    [SerializeField] private ExplodingBullet _explodingBulletPrefab;

    public void Initialize(IBullet bullet)
    {
        if (bullet == null)
            throw new ArgumentNullException(nameof(bullet));

        if (_explodingBulletPrefab == null)
            throw new NullReferenceException(nameof(_explodingBulletPrefab));

        if (bullet is Bullet bulletMono)
        {
            if (bulletMono.TryGetComponent(out ExplodingBullet explodingBullet))
            {
                explodingBullet.InitializeExploder(_explodingBulletPrefab.ExplosionSound);
                explodingBullet.SetBulletRepository(_explodingBulletPrefab.BulletRepository);
            }
        }
        else
        {
            throw new InvalidOperationException(nameof(bullet));
        }
    }
}