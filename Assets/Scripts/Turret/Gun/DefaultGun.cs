using System;
using System.Collections;
using UnityEngine;

public class DefaultGun : MonoBehaviour, IGun, IGunLoader
{
    private const float MinWaitingBetweenShots = 0.1f;
    private const float MaxWaitingBetweenShots = 2.0f;

    [SerializeField] private Muzzle _muzzle;

    private IGunMagazine _magazine;
    private ISound _sound;
    private WaitForSeconds _waitBetweenShots;
    private WaitForFixedUpdate _waitFixedUpdate;

    public event Action ShootingCompleted;

    public bool IsRecharged { get; private set; }

    private void OnValidate()
    {
        if (_muzzle == null)
            throw new NullReferenceException(nameof(_muzzle));
    }

    public void Initialize(IGunMagazine magazine, ISound sound, float waitingBetweenShots)
    {
        _magazine = magazine ?? throw new ArgumentNullException(nameof(magazine));
        _sound = sound ?? throw new ArgumentNullException(nameof(sound));

        if (waitingBetweenShots < MinWaitingBetweenShots || waitingBetweenShots > MaxWaitingBetweenShots)
            throw new ArgumentOutOfRangeException(nameof(waitingBetweenShots));

        _waitBetweenShots = new WaitForSeconds(waitingBetweenShots);
        _waitFixedUpdate = new WaitForFixedUpdate();

        IsRecharged = true;
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
        Vector3 startPoint = _muzzle.BulletSpawnPoint;

        IsRecharged = false;

        while (_magazine.HasFreeBullets)
        {
            IBullet bullet = _magazine.GetBullet();
            bullet.Move(startPoint, direction);

            _sound.Play();

            yield return _waitBetweenShots;
        }

        yield return StartCoroutine(CheckFullMagazine());
    }

    private IEnumerator CheckFullMagazine()
    {
        while (_magazine.IsFull == false)
            yield return _waitFixedUpdate;

        IsRecharged = true;

        ShootingCompleted?.Invoke();
    }
}
