using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet), typeof(Exploder))]
public class ExplodingBullet : MonoBehaviour, IBullet
{
    [SerializeField] private Scriptable.DamageImproverAttributes _damageImproverAttributes;

    private IBullet _bullet;
    private IExploder _exploder;
    private IBulletRepository _bulletRepository;

    public BulletType BulletType => _bullet.BulletType;
    public IBulletRepository BulletRepository => _bulletRepository;

    private void OnValidate()
    {
        if (_damageImproverAttributes == null)
            throw new System.NullReferenceException(nameof(_damageImproverAttributes));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IEnemy _))
        {
            Vector3 point = collision.contacts[0].point;
            _exploder.Explode(point);
            _bulletRepository.Put(_bullet);
        }
    }

    private void Start()
    {
        Bullet bullet = GetComponent<Bullet>();
        Exploder exploder = GetComponent<Exploder>();
        exploder.Initialize(bullet.DamageAttributes);
        bullet.ChangeDamage(_damageImproverAttributes);

        _bullet = bullet;
        _exploder = exploder;
    }

    public void Initialize(IBulletRepository bulletRepository)
    {
        _bulletRepository = bulletRepository ?? throw new System.ArgumentNullException(nameof(bulletRepository));
    }

    public void Move(Vector3 startPoint, Vector3 direction) => _bullet.Move(startPoint, direction);

    public void SetActive(bool isActive) => _bullet.SetActive(isActive);

    public void Gather(IBonus bonus) => _bullet.Gather(bonus);

    public bool TryGetBonuses(out List<IBonus> bonuses) => _bullet.TryGetBonuses(out bonuses);
}
