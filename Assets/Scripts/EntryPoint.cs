using System;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Player _player;

    [Header("Turret")]
    [SerializeField] private Tower _tower;
    [SerializeField] private TargetPoint _targetPoint;

    [Header("Gun")]
    [SerializeField] private GunAttributes _gunAttributes;
    [SerializeField] private DefaultGun _gun;
    [SerializeField] private BulletFactory _bulletFactory;

    private void OnValidate()
    {
        if (_player == null)
            throw new NullReferenceException(nameof(_player));

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

    private void Start()
    {
        ITurret turret = CreateTurret();
        _player.Initialize(turret);
    }

    private ITurret CreateTurret()
    {
        IGunMagazine gunMagazine = CreateGunMagazine(_gunAttributes.InitialCountBulltes);

        _gun.Initialize(gunMagazine, _gunAttributes.TimeBetweenShots);
        _tower.Initialize(_targetPoint);

        return new Turret(_gun, _tower);
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
