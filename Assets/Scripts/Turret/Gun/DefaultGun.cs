using System;
using System.Collections;
using UnityEngine;

public class DefaultGun : MonoBehaviour, IGun, IGunLoader
{
    [Header("Bullets")]
    [SerializeField] private BulletFactory _bulletFactory;
    [SerializeField, Min(1)] private int _startingCountBullets;

    [Header("Shot")]
    [SerializeField] private Transform _pointSpawnBullets;
    [SerializeField, Range(0.1f, 2.0f)] private float _waitingBetweenShots;

    private GunMagazine _magazine;
    private WaitForSeconds _waitBetweenShots;

    public event Action MagazineFilled;

    private void OnValidate()
    {
        if (_pointSpawnBullets == null)
            throw new NullReferenceException(nameof(_pointSpawnBullets));
    }

    private void Awake()
    {
        if (_bulletFactory == null)
            throw new NullReferenceException(nameof(_bulletFactory));

        _magazine = GetGunMagazine();

        _waitBetweenShots = new WaitForSeconds(_waitingBetweenShots);
    }

    private void OnEnable()
    {
        _magazine.Filled += MagazineFilled;
    }

    private void OnDisable()
    {
        _magazine.Filled -= MagazineFilled;
    }

    public void Shoot(Vector3 direction)
    {
        StartCoroutine(ShootBurst(direction));
    }

    public void AddBullet(IBullet bullet)
    {
        if (bullet == null)
            throw new ArgumentNullException(nameof(bullet));

        _magazine.AddBullet(bullet);
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
        GunMagazine magazine = new GunMagazine(_startingCountBullets);

        for (int i = 0; i < _startingCountBullets; i++)
        {
            IBullet bullet = _bulletFactory.Create(BulletType.Default);

            magazine.AddBullet(bullet);
        }

        return magazine;
    }
}
