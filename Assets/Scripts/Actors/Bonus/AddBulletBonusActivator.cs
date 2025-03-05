using System;
using UnityEngine;

public class AddBulletBonusActivator : MonoBehaviour, IBonusActivator
{
    public IBulletFactory BulletFactory { get; private set; }
    public IGunLoader GunLoader { get; private set; }

    public void Initialize(IBulletFactory bulletFactory, IGunLoader gunLoader)
    {
        BulletFactory = bulletFactory ?? throw new ArgumentNullException(nameof(bulletFactory));
        GunLoader = gunLoader ?? throw new ArgumentNullException(nameof(gunLoader));
    }

    public void Initialize(IBonusActivator bonusActivator)
    {
        if (bonusActivator == null) 
            throw new ArgumentNullException(nameof(bonusActivator));

        if (bonusActivator is AddBulletBonusActivator addBulletActivator)
        {
            BulletFactory = addBulletActivator.BulletFactory ?? throw new NullReferenceException(nameof(addBulletActivator.BulletFactory));
            GunLoader = addBulletActivator.GunLoader ?? throw new NullReferenceException(nameof(addBulletActivator.GunLoader));
        }
        else
        {
            throw new ArgumentOutOfRangeException();
        }
    }

    public void Activate()
    {
        IBullet bullet = BulletFactory.Create(BulletType.Default);
        GunLoader.AddBullet(bullet);
    }
}