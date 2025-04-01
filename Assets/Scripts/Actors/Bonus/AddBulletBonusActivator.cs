using System;
using UnityEngine;

public class AddBulletBonusActivator : MonoBehaviour, IBonusActivator
{
    private IBulletFactory _bulletFactory;
    private IGunLoader _gunLoader;

    public void Initialize(IBulletFactory bulletFactory, IGunLoader gunLoader)
    {
        _bulletFactory = bulletFactory ?? throw new ArgumentNullException(nameof(bulletFactory));
        _gunLoader = gunLoader ?? throw new ArgumentNullException(nameof(gunLoader));
    }

    public void Initialize(IBonusActivator bonusActivator)
    {
        if (bonusActivator == null) 
            throw new ArgumentNullException(nameof(bonusActivator));

        if (bonusActivator is AddBulletBonusActivator addBulletActivator)
        {
            _bulletFactory = addBulletActivator._bulletFactory ?? throw new NullReferenceException(nameof(addBulletActivator._bulletFactory));
            _gunLoader = addBulletActivator._gunLoader ?? throw new NullReferenceException(nameof(addBulletActivator._gunLoader));
        }
        else
        {
            throw new ArgumentOutOfRangeException();
        }
    }

    public void Activate()
    {
        IBullet bullet = _bulletFactory.Create(BulletType.Default);
        _gunLoader.AddBullet(bullet);
    }
}