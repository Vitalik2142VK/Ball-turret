public interface IGunMagazine
{
    public bool IsThereBullets { get; }
    public bool IsFull {  get; }

    public void AddBullet(IBullet bullet);

    public IBullet GetBullet();
}
