public class GunMagazine : IGunMagazine
{
    private IBulletRepository _bulletRepository;

    public GunMagazine(IBulletRepository bulletRepository)
    {
        _bulletRepository = bulletRepository ?? throw new System.ArgumentNullException(nameof(bulletRepository));
    }

    public bool AreThereBullets => _bulletRepository.AreThereFreeBullets;
    public bool IsFull => _bulletRepository.AreBulletsReturned;

    public void AddBullet(IBullet bullet)
    {
        _bulletRepository.Add(bullet);
    }

    public IBullet GetBullet() 
    {
        return _bulletRepository.Get();
    }
}
