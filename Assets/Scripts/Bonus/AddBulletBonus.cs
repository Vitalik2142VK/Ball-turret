using System;
using UnityEngine;

public class AddBulletBonus : MonoBehaviour, IBonus
{
    [SerializeField] private DefaultGun _gun;
    [SerializeField] private BulletFactory _bulletFactory;

    private IGunLoader _gunLoader;

    private void Awake()
    {
        if (_gun == null)
            throw new NullReferenceException(nameof(_gun));

        if (_bulletFactory == null)
            throw new NullReferenceException(nameof(_bulletFactory));

        _gunLoader = _gun;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bullet _))
        {
            _gunLoader.AddBullet(_bulletFactory.Create(BulletType.Default));

            Remove();
        }

    }

    private void Remove()
    {
        Destroy(gameObject);
    }
}
