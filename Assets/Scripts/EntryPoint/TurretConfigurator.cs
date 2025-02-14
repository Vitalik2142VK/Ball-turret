using System;
using UnityEngine;

public class TurretConfigurator : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private TargetPoint _targetPoint;
    [SerializeField] private GunAttributes _gunAttributes;
    [SerializeField] private DefaultGun _gun;
    [SerializeField] private BulletFactory _bulletFactory;

    public ITurret Turret { get; private set; }

    private void OnValidate()
    {
        if (_tower == null)
            throw new NullReferenceException(nameof(_tower));

        if (_targetPoint == null)
            throw new NullReferenceException(nameof(_targetPoint));

        if (_gunAttributes == null)
            throw new NullReferenceException(nameof(_gunAttributes));

        if (_gun == null)
            throw new NullReferenceException(nameof(_gun));

        if (_bulletFactory == null)
            throw new NullReferenceException(nameof(_bulletFactory));
    }

    public void Configure()
    {
        IGunMagazine gunMagazine = CreateGunMagazine(_gunAttributes.InitialCountBulltes);

        _gun.Initialize(gunMagazine, _gunAttributes.TimeBetweenShots);
        _tower.Initialize(_targetPoint);

        Turret = new Turret(_gun, _tower);
    }

    private IGunMagazine CreateGunMagazine(int initialCountBullets)
    {
        GunMagazine magazine = new GunMagazine(initialCountBullets);

        for (int i = 0; i < initialCountBullets; i++)
        {
            IBullet bullet = _bulletFactory.Create(BulletType.Default);

            magazine.AddBullet(bullet);
        }

        return magazine;
    }
}