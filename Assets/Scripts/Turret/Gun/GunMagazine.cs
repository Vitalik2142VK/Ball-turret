using System.Collections.Generic;

public class GunMagazine
{
    private List<IBullet> _bullets;

    public GunMagazine(int initialQuantity)
    {
        _bullets = new List<IBullet>(initialQuantity);
    }

    public bool IsThereBullets => _bullets.Count > 0;

    public void AddBullet(IBullet bullet)
    {
        _bullets.Add(bullet);
    }

    public IBullet GetBullet() 
    {
        if (IsThereBullets == false)
            throw new System.InvalidOperationException($"IsThereBullets == {IsThereBullets}");

        IBullet bullet = _bullets[0];
        bullet.SetActive(true);
        bullet.Finished += OnPutBullet;

        _bullets.RemoveAt(0);

        return bullet;
    }

    private void OnPutBullet(IBullet bullet)
    {
        bullet.SetActive(false);
        bullet.Finished -= OnPutBullet;

        _bullets.Add(bullet);
    }
}
