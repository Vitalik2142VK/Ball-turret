using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class AddBulletBonus : MonoBehaviour, IBonus, IActor
{
    [SerializeField] private BulletFactory _bulletFactoryMono;
    [SerializeField] private DefaultGun _gun;

    private IMover _mover;
    private IBulletFactory _bulletFactory;
    private IGunLoader _gunLoader;

    public IMover Mover => _mover;
    public bool IsEnable { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<Mover>();

        _bulletFactory = _bulletFactoryMono;
        _gunLoader = _gun;
    }

    private void OnEnable()
    {
        IsEnable = true;
    }

    private void OnDisable()
    {
        IsEnable = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IBonusGatherer gatheringBonus))
        {
            gatheringBonus.Gather(this);

            Destroy();
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

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
