public interface IBulletRepository
{
    public bool AreThereFreeBullets {  get; }

    public bool AreBulletsReturned { get; }

    public void Add(IBullet bullet);

    public IBullet Get();

    public void Put(IBullet bullet);
}