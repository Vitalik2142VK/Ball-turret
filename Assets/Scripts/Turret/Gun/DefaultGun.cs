﻿using System;
using System.Collections;
using UnityEngine;

public class DefaultGun : MonoBehaviour, IGun, IGunLoader
{
    private const float MinWaitingBetweenShots = 0.1f;
    private const float MaxWaitingBetweenShots = 2.0f;

    [SerializeField] private Transform _pointSpawnBullets;

    private IGunMagazine _magazine;
    private WaitForSeconds _waitBetweenShots;

    public bool IsShooting { get; private set; }

    private void OnValidate()
    {
        if (_pointSpawnBullets == null)
            throw new NullReferenceException(nameof(_pointSpawnBullets));
    }

    public void Initialize(IGunMagazine magazine, float waitingBetweenShots)
    {
        _magazine = magazine ?? throw new ArgumentNullException(nameof(magazine));

        if (waitingBetweenShots < MinWaitingBetweenShots || waitingBetweenShots > MaxWaitingBetweenShots)
            throw new ArgumentOutOfRangeException(nameof(waitingBetweenShots));

        _waitBetweenShots = new WaitForSeconds(waitingBetweenShots);
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

        IsShooting = true;

        while (_magazine.IsThereBullets)
        {
            IBullet bullet = _magazine.GetBullet();
            bullet.Move(startPoint, direction);

            yield return _waitBetweenShots;
        }

        yield return StartCoroutine(CheckFullMagazine());
    }

    private IEnumerator CheckFullMagazine()
    {
        while (_magazine.IsFull == false)
        {
            yield return null;
        }

        IsShooting = false;
    }
}
