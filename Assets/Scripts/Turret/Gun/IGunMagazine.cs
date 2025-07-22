public interface IGunMagazine
{
    public bool HasFreeBullets { get; }
    public bool IsFull {  get; }

    public void AddBullet(IBullet bullet);

    public IBullet GetBullet();
}
