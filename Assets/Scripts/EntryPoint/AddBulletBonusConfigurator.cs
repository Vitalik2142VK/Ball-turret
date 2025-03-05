using System;
using UnityEngine;

public class AddBulletBonusConfigurator : BonusConfigurator
{
    [SerializeField] private BulletFactory _bulletFactory;
    [SerializeField] private DefaultGun _gun;

    private void OnValidate()
    {
        if (_bulletFactory == null)
            throw new NullReferenceException(nameof(_bulletFactory));

        if (_gun == null)
            throw new NullReferenceException(nameof(_gun));
    }

    public override void Configure()
    {
        if (BonusPrefab.TryGetComponent(out AddBulletBonusActivator addBulletBonusActivator) == false)
            throw new InvalidOperationException();

        addBulletBonusActivator.Initialize(_bulletFactory, _gun);
    }
}
