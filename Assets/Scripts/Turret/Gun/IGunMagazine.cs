public interface IGunMagazine
{
    public bool AreThereBullets { get; }
    public bool IsFull {  get; }

    public void AddBullet(IBullet bullet);

    public IBullet GetBullet();
}
