using System;
using UnityEngine;

public class AddBulletBonus : MonoBehaviour, IBonus
{
    [SerializeField] private BulletFactory _bulletFactoryMono;
    [SerializeField] private DefaultGun _gun;

    private IBulletFactory _bulletFactory;
    private IGunLoader _gunLoader;

    private void Awake()
    {
        _bulletFactory = _bulletFactoryMono;
        _gunLoader = _gun;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IBonusGatherer gatheringBonus))
        {
            gatheringBonus.Gather(this);

            Remove();
        }
    }

    public void Initialize(IBulletFactory bulletFactory, IGunLoader gunLoader)
    {
        _bulletFactory = bulletFactory ?? throw new ArgumentNullException(nameof(bulletFactory));
        _gunLoader = gunLoader ?? throw new ArgumentNullException(nameof(gunLoader));
    }

    public void Activate()
    {
        IBullet bullet = _bulletFactory.Create(BulletType.Default);
        _gunLoader.AddBullet(bullet);
    }

    private void Remove()
    {
        Destroy(gameObject);
    }
}
