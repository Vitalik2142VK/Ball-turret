using System.Collections.Generic;
using UnityEngine;

//Remove if don't use;
[RequireComponent(typeof(Bullet))]
public class DefaultBullet : MonoBehaviour, IBullet
{
    private IBullet _bullet;

    public BulletType BulletType => _bullet.BulletType;

    private void Awake()
    {
        _bullet = GetComponent<Bullet>();
    }

    public void Move(Vector3 startPoint, Vector3 direction)
    {
        _bullet.Move(startPoint, direction);
    }

    public void SetActive(bool isActive)
    {
        _bullet.SetActive(isActive);
    }

    public void Gather(IBonus bonus)
    {
        _bullet.Gather(bonus);
    }

    public bool TryGetBonuses(out List<IBonus> bonuses)
    {
        return _bullet.TryGetBonuses(out bonuses);
    }
}
