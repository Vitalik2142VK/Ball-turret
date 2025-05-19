using System;
using UnityEngine;

public class SpecialBulletInitializer : MonoBehaviour, IBulletInitializer
{
    public void Initialize(IBullet bullet)
    {
        if (bullet == null)
            throw new System.ArgumentNullException(nameof(bullet));

        if (bullet is Bullet bulletMono)
        {
            if (bulletMono.TryGetComponent(out IInitializer initializer))
                initializer.Initialize();
        }
        else
        {
            throw new InvalidOperationException(nameof(bullet));
        }
    }
}
