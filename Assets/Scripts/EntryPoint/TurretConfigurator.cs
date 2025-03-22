using System;
using UnityEngine;

public class TurretConfigurator : MonoBehaviour
{
    [Header("Turret")]
    [SerializeField] private Tower _tower;
    [SerializeField] private TargetPoint _targetPoint;
    [SerializeField] private DefaultGun _gun;
    [SerializeField] private HealthBar _healthBar;

    [Header("Bullets")]
    [SerializeField] private BulletFactory _bulletFactory;

    [Header("Attributes")]
    [SerializeField] private GunAttributes _gunAttributes;
    [SerializeField] private HealthAttributes _turretHealthAttributes;

    private Turret _turret;

    public ITurret Turret => _turret;

    private void OnValidate()
    {
        if (_tower == null)
            throw new NullReferenceException(nameof(_tower));

        if (_targetPoint == null)
            throw new NullReferenceException(nameof(_targetPoint));

        if (_gun == null)
            throw new NullReferenceException(nameof(_gun));

        if (_bulletFactory == null)
            throw new NullReferenceException(nameof(_bulletFactory));

        if (_healthBar == null)
            throw new NullReferenceException(nameof(_healthBar));

        if (_gunAttributes == null)
            throw new NullReferenceException(nameof(_gunAttributes));

        if (_turretHealthAttributes == null)
            throw new NullReferenceException(nameof(_turretHealthAttributes));
    }

    private void OnDisable()
    {
        _turret.Disable();
    }

    public void Configure()
    {
        IGunMagazine gunMagazine = CreateGunMagazine(_gunAttributes.InitialCountBulltes);

        _gun.Initialize(gunMagazine, _gunAttributes.TimeBetweenShots);
        _tower.Initialize(_targetPoint);

        IHealth health = new Health(_turretHealthAttributes, _healthBar);
        health.Restore();

        _turret = new Turret(_gun, _tower, health);
        _turret.Enable();
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