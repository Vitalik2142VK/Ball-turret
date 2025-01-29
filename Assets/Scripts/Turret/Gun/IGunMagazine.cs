using System;

public interface IGunMagazine
{
    public event Action Filled;

    public bool IsThereBullets { get; }

    public void AddBullet(IBullet bullet);

    public IBullet GetBullet();
}
