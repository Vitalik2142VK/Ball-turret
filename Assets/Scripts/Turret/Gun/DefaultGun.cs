using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BulletFactory))]
public class DefaultGun : MonoBehaviour, IGun
{
    [SerializeField] private Transform _pointSpawnBullets;
    [SerializeField, Range(0.1f, 2.0f)] private float _waitingBetweenShots;
    [SerializeField, Min(1)] private int _coutnBullet;

    private BulletFactory _bulletFactory;
    private GunMagazine _magazine;
    private WaitForSeconds _waitBetweenShots;

    private void OnValidate()
    {
        if (_pointSpawnBullets == null)
            throw new NullReferenceException(nameof(_pointSpawnBullets));
    }

    private void Awake()
    {
        _bulletFactory = GetComponent<BulletFactory>();
        _magazine = GetGunMagazine();

        _waitBetweenShots = new WaitForSeconds(_waitingBetweenShots);
    }

    public void Shoot(Vector3 direction)
    {
        StartCoroutine(ShootBurst(direction));
    }

    private IEnumerator ShootBurst(Vector3 direction)
    {
        Vector3 startPoint = _pointSpawnBullets.position;

        while (_magazine.IsThereBullets)
        {
            IBullet bullet = _magazine.GetBullet();
            bullet.Move(startPoint, direction);

            yield return _waitBetweenShots;
        }
    }

    private GunMagazine GetGunMagazine()
    {
        GunMagazine magazine = new GunMagazine(_coutnBullet);

        for (int i = 0; i < _coutnBullet; i++)
            magazine.AddBullet(_bulletFactory.Create(BulletType.Default));

        return magazine;
    }
}
