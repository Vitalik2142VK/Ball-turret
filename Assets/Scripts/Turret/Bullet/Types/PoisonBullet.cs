using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet), typeof(PoisonBulletDebuff))]
public class PoisonBullet : MonoBehaviour, IBullet
{
    [SerializeField] private Scriptable.DamageImproverAttributes _damageImproverAttributes;

    private IBullet _bullet;
    private IBulletDebuff _bulletDebaff;

    public BulletType BulletType => _bullet.BulletType;

    private void OnValidate()
    {
        if (_damageImproverAttributes == null)
            throw new System.NullReferenceException(nameof(_damageImproverAttributes));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IEnemy enemy))
            _bulletDebaff.ApplyDebuff(enemy);
    }

    private void Start()
    {
        Bullet bullet = GetComponent<Bullet>();
        bullet.ChangeDamage(_damageImproverAttributes);

        PoisonBulletDebuff bulletDebaff = GetComponent<PoisonBulletDebuff>();
        bulletDebaff.Initialize(bullet.DamageAttributes);

        _bullet = bullet;
        _bulletDebaff = bulletDebaff;
    }

    public void Move(Vector3 startPoint, Vector3 direction) => _bullet.Move(startPoint, direction);

    public void SetActive(bool isActive) => _bullet.SetActive(isActive);

    public void Gather(IBonus bonus) => _bullet.Gather(bonus);

    public bool TryGetBonuses(out List<IBonus> bonuses) => _bullet.TryGetBonuses(out bonuses);
}
