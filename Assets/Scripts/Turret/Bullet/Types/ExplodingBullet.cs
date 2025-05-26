using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet), typeof(Exploder), typeof(BulletPhysics))]
public class ExplodingBullet : MonoBehaviour, IBullet, IInitializer
{
    [SerializeField] private Scriptable.DamageImproverAttributes _damageImproverAttributes;

    private IBullet _bullet;
    private IExploder _exploder;
    private IBulletRepository _bulletRepository;
    private IBulletPhysics _bulletPhysics;

    public BulletType BulletType => _bullet.BulletType;
    public IBulletRepository BulletRepository => _bulletRepository;

    private void OnValidate()
    {
        if (_damageImproverAttributes == null)
            throw new System.NullReferenceException(nameof(_damageImproverAttributes));
    }

    private void Awake()
    {
        _bulletPhysics = GetComponent<IBulletPhysics>();
    }

    private void OnEnable()
    {
        _bulletPhysics.EnteredCollision += OnExplode;
    }

    private void OnDisable()
    {
        _bulletPhysics.EnteredCollision -= OnExplode;
    }

    public void Initialize()
    {
        Bullet bullet = GetComponent<Bullet>();
        Exploder exploder = GetComponent<Exploder>();

        exploder.Initialize(bullet.DamageAttributes);
        bullet.ChangeDamage(_damageImproverAttributes);

        _bullet = bullet;
        _exploder = exploder;
    }

    public void SetBulletRepository(IBulletRepository bulletRepository)
    {
        _bulletRepository = bulletRepository ?? throw new System.ArgumentNullException(nameof(bulletRepository));
    }

    public void Move(Vector3 startPoint, Vector3 direction) => _bullet.Move(startPoint, direction);

    public void SetActive(bool isActive) => _bullet.SetActive(isActive);

    public void Gather(IBonus bonus) => _bullet.Gather(bonus);

    public bool TryGetBonuses(out IReadOnlyCollection<IBonus> bonuses) => _bullet.TryGetBonuses(out bonuses);

    private void OnExplode(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out IEnemy _))
        {
            _exploder.Explode(transform.position);
            _bulletRepository.Put(_bullet);
        }
    }
}
