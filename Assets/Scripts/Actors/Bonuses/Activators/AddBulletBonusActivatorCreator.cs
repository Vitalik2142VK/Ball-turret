using System;
using UnityEngine;

public class AddBulletBonusActivatorCreator : MonoBehaviour, IBonusActivatorCreator
{
    [SerializeField] private BulletFactory _bulletFactory;
    [SerializeField] private Gun _gun;
    [SerializeField] private BulletType _bulletType;

    private void OnValidate()
    {
        if (_bulletFactory == null)
            throw new NullReferenceException(nameof(_bulletFactory));

        if (_gun == null)
            throw new NullReferenceException(nameof(_gun));
    }
    public IBonusActivator Create()
    {
        return new AddBulletBonusActivator(_bulletFactory, _gun, _bulletType);
    }
}
