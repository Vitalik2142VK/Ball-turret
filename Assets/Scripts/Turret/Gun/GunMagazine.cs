using System;
using System.Collections.Generic;

public class GunMagazine : IGunMagazine
{
    private List<IBullet> _bullets;
    private int _countBullets;

    public GunMagazine(int countBullets)
    {
        _countBullets = countBullets;
        _bullets = new List<IBullet>(_countBullets);
    }

    public bool IsThereBullets => _bullets.Count > 0;
    public bool IsFull => _bullets.Count == _countBullets;

    public void AddBullet(IBullet bullet)
    {
        _bullets.Add(bullet);
        _countBullets = _bullets.Count;
    }

    public IBullet GetBullet() 
    {
        if (IsThereBullets == false)
            throw new InvalidOperationException("The count of bullets is 0");

        IBullet bullet = _bullets[0];
        bullet.SetActive(true);
        bullet.Finished += OnPutBullet;

        _bullets.RemoveAt(0);

        return bullet;
    }

    private void OnPutBullet(IBullet bullet)
    {
        if (bullet == null)
            throw new ArgumentNullException(nameof(bullet));

        bullet.SetActive(false);
        bullet.Finished -= OnPutBullet;

        _bullets.Add(bullet);
    }
}
